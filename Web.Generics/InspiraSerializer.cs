using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Web.Generics
{
    public class InspiraSerializer
    {
        StringBuilder sb = new StringBuilder();

        internal string Serialize(object model)
        {
            AppendObject(model);
            return sb.ToString();
        }

        private void PopIndent()
        {
            indent--;
        }

        private void PushIndent()
        {
            indent++;
        }

        Boolean first = false;
        private void WriteLine(string p)
        {
            Write(p);
            this.sb.AppendLine();
            first = true;
        }

        int indent = 0;
        private void AppendObject(object model)
        {
            if (indent > 5) return;

            Type type = model.GetType();
            if (model is IEnumerable || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>)) || type.IsArray)
            {
                WriteLine("[");
                PushIndent();
                foreach (object obj in (IEnumerable)model)
                {
                    AppendObject(obj);
                    WriteLine(",");
                }
                PopIndent();
                WriteLine("]");
            }
            else
            {
                WriteLine("{");
                foreach (PropertyInfo p in model.GetType().GetProperties())
                {
                    if (p.GetIndexParameters().Length > 0) continue;

                    //if (p.PropertyType == typeof(IEnumerable) || (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)) || p.PropertyType.IsArray)
                    //    continue;
                    PushIndent();
                    Write("'" + p.Name + "': ");
                    if (p.PropertyType == typeof(IEnumerable) || (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)) || p.PropertyType.IsArray)
                    {
                        foreach (Object o in (IEnumerable)p.GetValue(model, null))
                        {
                            AppendObject(o);
                        }
                    }
                    else if (p.PropertyType.Namespace.StartsWith("System"))
                    {
                        Write("'");
                        Write(Convert.ToString(p.GetValue(model, null)));
                        Write("'");
                    }
                    else
                    {
                        AppendObject(p.GetValue(model, null));
                    }
                    WriteLine(",");
                    PopIndent();
                }
                WriteLine("}");
            }
        }

        private void WriteLine()
        {
            sb.AppendLine();
        }

        private void Write(string p)
        {
            if (first) sb.Append(new String('\t', indent));
            sb.Append(p);
            first = false;
        }
    }
}

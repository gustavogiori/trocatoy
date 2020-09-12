using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Infrastructure.Query
{
    public static class QueryService<T>
    {
        public static Expression<Func<T, bool>> GetCriteria(string campo, object valor)
        {
            if (string.IsNullOrEmpty(campo))
                throw new Exception("Argumento invalido, o campo para busca deve ser preenchido!");
            if (valor is null)
                valor = GConvert.ToString(valor);

            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            // obtem tipo da propriedade
            Type type;
            var _propertyNames = campo.Split('.');
            if (_propertyNames.Length > 1)
                type = typeof(T).GetProperty(_propertyNames[0]).PropertyType.GetProperty(_propertyNames[1]).PropertyType;
            else
                type = typeof(T).GetProperty(campo,BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).PropertyType;
            // cria Expression para o campo
            MemberExpression propertyExpression = Expression.PropertyOrField(param, campo);
            // cria Expression para o valor
            ConstantExpression valueExpression = Expression.Constant(Convert.ChangeType(valor, type), type);
            MethodInfo methodInfo;
            if (type == typeof(string))
                methodInfo = type.GetMethod("Contains", new[] { type });
            else
                methodInfo = type.GetMethod("Equals", new[] { type });

            var predicate = Expression.Lambda<Func<T, bool>>(
                Expression.Call(propertyExpression, methodInfo, valueExpression)
            , param);

            return predicate;

        }
    }
}

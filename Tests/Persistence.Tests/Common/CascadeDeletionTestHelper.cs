using AutoMapper.Internal;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Tests.Common
{
    public static class CascadeDeletionTestHelper
    {
        public static PropertyInfo[] Properties(this Type type, DataContext context, Guid Id)
        {
            var entity = context.Find(type, Id);
            var entityProps = entity.GetType().GetProperties();

            return entityProps;
        }

        public static IDictionary<Guid, Type> RelatedEntities(this PropertyInfo[] entityProps, object entity)
        {
            var ex = typeof(User);
            var relatedEntities = new Dictionary<Guid, Type>();

            foreach (var prop in entityProps)
            {
                if (prop.PropertyType.IsGenericType)
                {
                    var collectionProperty = prop.GetValue(entity) as IEnumerable<object>;

                    if (collectionProperty is null)
                        continue;

                    foreach (var member in collectionProperty)
                    {
                        var memberId = Guid.Parse(member.GetType().GetProperty("Id").GetValue(member).ToString());
                        relatedEntities.Add(memberId, member.GetType().GetTypeInfo());
                    }
                }
                if ((prop.PropertyType.IsClass && prop.PropertyType != typeof(String)))
                {
                    var propertyId = Guid.Parse(prop.GetValue(entity)
                        .GetType().GetProperty("Id").GetValue(prop.GetValue(entity)).ToString());
                    relatedEntities.Add(propertyId, prop.GetValue(entity).GetType());
                }
            }

            return relatedEntities;
        }

        public static bool IfAllEntitiesHasBeenDeleted<T>
            (this IDictionary<Guid, Type> relatedEntities, DataContext context)
        {
            bool expectedResult;

            switch (typeof(T))
            {
                case Type type when type == typeof(Budget):
                    foreach (var entity in relatedEntities)
                    {
                        if (entity.Value == typeof(User))
                            expectedResult = context.Find(entity.Value, entity.Key.ToString()) is not null;
                        else
                            expectedResult = context.Find(entity.Value, entity.Key) is null;
                    }
                    break;

                case Type type when type == typeof(Account):
                    foreach (var entity in relatedEntities)
                    {
                        if (entity.Value == typeof(User))
                            expectedResult = context.Find(entity.Value, entity.Key.ToString()) is not null;
                        else if (entity.Value == typeof(Budget))
                            expectedResult = context.Find(entity.Value, entity.Key) is not null;
                        else
                            expectedResult = context.Find(entity.Value, entity.Key) is null;
                    }
                    break;

                case Type type when type == typeof(Transaction):
                    foreach (var entity in relatedEntities)
                    {
                        if (entity.Value == typeof(User))
                            expectedResult = context.Find(entity.Value, entity.Key.ToString()) is not null;
                        else if (entity.Value == typeof(Budget))
                            expectedResult = context.Find(entity.Value, entity.Key) is not null;
                        else if (entity.Value == typeof(FutureTransaction))
                            expectedResult = context.Find(entity.Value, entity.Key) is not null;
                        else
                            expectedResult = context.Find(entity.Value, entity.Key) is null;
                    }
                    break;
            }

            return true;
        }
    }
}
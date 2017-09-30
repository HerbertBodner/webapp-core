using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaCore.TemplateMgmt.Services.Attributes;

namespace WaCore.TemplateMgmt.Services
{
    static class InstanceCreatorHelper
    {
        public static object CreateInstanceRecursively(Type modeltype, int maxHierarchyDepth)
        {
            var instance = Activator.CreateInstance(modeltype);

            CreateInstanceAndFillWithTemplateTestValuesRecursive(instance, maxHierarchyDepth);

            return instance;
        }

        private static void CreateInstanceAndFillWithTemplateTestValuesRecursive(object instance, int hierarchyDepth)
        {
            FillInstanceWithTemplateTestValues(instance);

            if (hierarchyDepth == 0)
                return;

            // get those properties from the instance, which are a class, are not primitive types and have a default constructor (no constructor parameters)
            var propsOfClassTypes = instance.GetType().GetProperties().Where(x =>
                    x.PropertyType.IsClass && 
                    !x.PropertyType.IsPrimitive &&
                    x.PropertyType.GetConstructors().Any(y => y.GetParameters().Count() == 0));

            // for those properties we are going to create an instance and assign it to the property
            // then we do the same thing recursively with the properties of that new instance
            foreach (var classType in propsOfClassTypes)
            {
                var propertyInstance = Activator.CreateInstance(classType.PropertyType);

                classType.SetValue(instance, propertyInstance);

                CreateInstanceAndFillWithTemplateTestValuesRecursive(propertyInstance, hierarchyDepth - 1);
            }
        }

        private static void FillInstanceWithTemplateTestValues(object instance)
        {
            // find properties, which have the TemplateTestAttribute assigned
            var propertiesWithAttribute = instance.GetType().GetProperties()
                .Select(x =>
                    new {
                        PropertyInfo = x,
                        MemberInfo = (TemplateTestAttribute)x.GetCustomAttributes(false).FirstOrDefault(y => y is TemplateTestAttribute)
                    })
                .Where(x => x.MemberInfo != null);

            // read the value from the TemplateTestAttribute and assign it to the property
            foreach (var prop in propertiesWithAttribute)
            {
                var value = prop.MemberInfo.Value;
                prop.PropertyInfo.SetValue(instance, value);
            }
        }
    }
}

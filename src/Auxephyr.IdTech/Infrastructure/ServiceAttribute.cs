using System;

namespace Auxephyr.IdTech.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ModelAttribute : Attribute
    {
    }
}
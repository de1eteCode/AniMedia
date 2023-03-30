using System.Reflection;

namespace AniMedia.Application;

public static class EnumHelper {

    public static T GetAttributeOfType<T>(this Enum e) where T : Attribute {
        return e.GetType().GetTypeInfo().DeclaredMembers.First(s => s.Name.Equals(e.ToString()))
            .GetCustomAttribute<T>() ?? throw new Exception($"Not found attribute type of '{typeof(T).Name}'");
    }
}
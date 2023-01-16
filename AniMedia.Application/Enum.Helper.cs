using System.Reflection;

namespace AniMedia.Application;

public static class EnumHelper {

    public static T GetAttributeOfType<T>(this Enum enumVal)
        where T : Attribute {
        var typeInfo = enumVal.GetType().GetTypeInfo();
        var v = typeInfo.DeclaredMembers.First(x => x.Name == enumVal.ToString());
        return v.GetCustomAttribute<T>();
    }
}
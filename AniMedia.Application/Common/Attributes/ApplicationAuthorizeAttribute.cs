namespace AniMedia.Application.Common.Attributes;

/// <summary>
/// Атрибут сведетельстующий что пользователь должен быть авторизирован на уровне приложения
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class ApplicationAuthorizeAttribute : Attribute {

    public ApplicationAuthorizeAttribute() {
    }
}
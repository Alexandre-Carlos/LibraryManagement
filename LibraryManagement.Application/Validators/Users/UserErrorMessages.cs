namespace LibraryManagement.Application.Validators.Users
{
    public static class UserErrorMessages
    {
        public static string NameEmpty = "Nome tem que ser preenchido!";
        public static string NameMaximuLength = "O Nome pode ter no máximo 100 caracteres";

        public static string EmailEmpty = "Email tem que ser preenchido!";
        public static string EmailNotStandard = "Email deve conter apenas números ou hífens para separadores!";
    }
}

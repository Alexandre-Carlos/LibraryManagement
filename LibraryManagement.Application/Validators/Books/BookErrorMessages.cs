namespace LibraryManagement.Application.Validators.Books
{
    public static class BookErrorMessages
    {
        public static string TitleEmpty = "Título tem que ser preenchido!";
        public static string TitleMaximuLength = "O Título pode ter no máximo 100 caracteres";

        public static string AuthorEmpty = "Autor tem que ser preenchido!";
        public static string AuthorMaximuLength = "Nome do Autor pode ter no máximo 100 caracteres";

        public static string IsbnEmpty = "Isbn tem que ser preenchido!";
        public static string IsbnMinimumLength = "Isbn deve ter 13 caracteres!";
        public static string IsbnNotStandard = "Isbn deve conter apenas números ou hífens para separadores!";

        public static string YearPublishedEmpty = "Ano de publicação tem que ser preenchido!";
        public static string YearPublishedGreaterOrEqual = "Ano de publicação deve ser maior que 1700!";
        public static string YearPublishedLessOrEqual = "Ano de publicação não pode ser maior que o ano atual";

        public static string QuantityEmpty = "Quantidade tem que ser preenchida!";
        public static string QuantityGreaterOrEqual = "Quantidade deve ser maior que zero!";
        public static string QuantityLessOrEqual = "Quantidade não pode ser maior que 30!";
    }
}
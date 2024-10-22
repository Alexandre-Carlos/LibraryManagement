namespace LibraryManagement.Application.Services.Mail
{
    public static class TemplateMail
    {
        public static string TemplateNotification = "<!DOCTYPE html>\r\n<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n   <meta charset=\"utf-8\" />\r\n   <title></title>\r\n   <style type=\"text/css\">\r\n      body {\r\n         margin: 20px 20px;\r\n         font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;\r\n      }\r\n   </style>\r\n</head>\r\n<body>\r\n   Prezado(a) #ReceiverName#,\r\n   <p>\r\n   #Message#\r\n   </p>\r\n   <p>\r\n   #Livro#\r\n   </p>\r\n   <p>\r\n   #Description#\r\n   </p>\r\n   Atenciosamente,<br />\r\n   Setor de Emprestimo de Livros\r\n</body>\r\n</html>";
    }
}

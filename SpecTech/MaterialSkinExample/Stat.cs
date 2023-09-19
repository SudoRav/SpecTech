using System.IO;

namespace MaterialSkinExample
{
    class StatUser
    {
        public static int ID;
        public static string login;
        public static string password;
        public static string F;
        public static string I;
        public static string O;
        public static string email;
        public static string phone;
        public static string post;

        public static int ID_post;
        public static string access1;
        public static string access2;
        public static string access3;
        public static string access4;
        public static string access5;
        public static string access6;
    }

    class StatPost
    {
        public static int ID_post;
        public static string postName;
        public static string access1;
        public static string access2;
        public static string access3;
        public static string access4;
        public static string access5;
        public static string access6;
        public static int salary;
    }

    class StatUser_sel
    {
        public static string query;
        public static int ID;
        public static string login;
        public static string password;
        public static string F;
        public static string I;
        public static string O;
        public static string email;
        public static string phone;
        public static string post;
    }

    class StatTech
    {
        public static int ID;
        public static string name;
        public static MemoryStream photo;
        public static string type;
        public static int typeID;
        public static string desc;
        public static double price;
        public static string status_leas;
        public static string status_rep;
        public static string rep_text;
        public static int discount;
    }

    class StatLeas
    {
        public static int ID;
        public static string FI_user;
        public static string address;
        public static string summ;
    }
}

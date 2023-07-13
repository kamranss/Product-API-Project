using System.Xml.Linq;

namespace WebApplication1.Extension
{
    public static class UserExtension
    {
        public static int BirthdayToAge( this DateTime date) // extending method to datatime
        {
            int age = DateTime.Now.Year - date.Year;
            return age;
        }

        public static string FullName(this String name, String surname)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (surname == null)
                throw new ArgumentNullException(nameof(surname));
            
            string fullname = String.Concat(name, surname);
            return fullname;
        }
    }
}

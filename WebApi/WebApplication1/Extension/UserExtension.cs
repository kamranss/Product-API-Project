namespace WebApplication1.Extension
{
    public static class UserExtension
    {
        public static int BirthdayToAge( this DateTime date) // extending method to datatime
        {
            int age = DateTime.Now.Year - date.Year;
            return age;
        }
    }
}

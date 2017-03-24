namespace ConsoleApplication1
{
    internal class submatrix__mes
    {

        int term_min;
        int term_max;

        public void assign_min(string a)
        {
            term_min = int.Parse(a);
        }

        public void assign_parameters(string a)
        {
            a = a.Replace(" ", "");
            term_max = int.Parse(a.Split('-')[1]);
            term_min = int.Parse(a.Split('-')[0]);
        }
    }
}   
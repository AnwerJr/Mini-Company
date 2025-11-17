namespace WebApplication1.ViewModel
{

    // خاصه بالحجات المشتركه بين ال model , view 
    public class EmpDeptColorTempMsgBranchesViewModel
    {
        public String EmpName{ get; set; }
        public String DepName{ get; set; }
        public List<String> Branches{ get; set; }
        public String Color{ get; set; }
        public int Temp{ get; set; }
        public String Msg{ get; set; }
    }
}

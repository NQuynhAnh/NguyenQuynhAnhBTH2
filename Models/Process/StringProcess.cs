using System.Text.RegularExpressions;

namespace NguyenQuynhAnhBTH2.Models.Process
{
    public class StringProcess
    {
        public string AutoGenerateCode(string strInput)
        {
            string strResult = "", numPart = "", strPart = "";
            //tach phan so tu strInput
            //Vd: strInput = "STD001" => numPart = "001"
            numPart = Regex.Match(strInput,@"/d+").Value;
            //tach phan chu tu strInput
            //strPart = STD
            strPart = Regex.Match(strInput,@"/D+").Value;
            //tang phan so len 1 don vi
            int intPart = (Convert.ToInt32(numPart)+1);
            //bo sung cac ky tu 0 con thieu
            for (int i = 0; i < numPart.Length - intPart.ToString().Length; i++)
            {
                strPart +="0";
            }
            strResult = strPart + intPart ;
            return strResult;
        }
    }
}
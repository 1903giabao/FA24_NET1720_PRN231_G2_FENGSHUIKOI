using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENGSHUIKOI.Data.Enums
{
    public class ElementObjects
    {
        public static readonly Dictionary<int, string> ThienCan = new Dictionary<int, string>
        {
        { 0, "Giáp" }, {1, "Ất"} , {2, "Bính" }, {3, "Đinh"} , {4, "Mậu"}, {5, "Kỉ"}, {6,"Canh"}, {7,"Tân"}, {8, "Nhâm"}, {9,"Quý"}
        } ;
        public static readonly Dictionary<int, string> DiaChi = new Dictionary<int, string>
        {
        { 0, "Tý" }, {1, "Sửu"} , {2, "Dần" }, {3, "Mão"} , {4, "Thìn"}, {5, "Tị"}, {6,"Ngọ"}, {7,"Mùi"}, {8, "Thân"}, {9,"Dậu"} , {10,"Tuất"}, {11, "Hợi"}
        };
        public static readonly Dictionary<string, int> ConventionThienCan = new Dictionary<string, int>
        {
            {"Giáp",1 },{"Ất",1 },
            {"Bính",2 }, {"Đinh",2},
            {"Mậu",3 }, {"Kỷ",3},
            {"Canh",4 }, {"Tân",4},
            {"Nhâm",5 }, {"Quý",5}
        };
        public static readonly Dictionary<string, int> ConventionDiaChi = new Dictionary<string, int>
        {
            {"Tý",0 },{"Sửu",0 },{"Ngọ",0 }, {"Mùi",0 },
            {"Dần",1 }, {"Mão",1},{"Thân",1},{"Dậu",1},
            {"Thìn",2 }, {"Tỵ",2}, {"Tuất",2}, {"Hợi",2}
        };
        public static readonly Dictionary<int, string> MaleValueInElement = new Dictionary<int, string>
        {
            {1,"Khảm" }, {2, "Ly"}, {3, "Cấn"}, {4,"Đoài"}, {5, "Càn"}, {6,"Khôn"}, {7, "Tốn"}, {8, "Chấn"}, {0, "Khôn"}
        };
        public static readonly Dictionary<int, string> FemaleValueInElement = new Dictionary<int, string>
        {
            {6,"Khảm" }, {5, "Ly"}, {1, "Cấn"}, {3,"Đoài"}, {2, "Càn"}, {7,"Khôn"}, {0, "Tốn"}, {8, "Chấn"}, {4, "Cấn"}
        };

        public static readonly Dictionary<int, string> ElementValue = new Dictionary<int, string>
        {
            {1,"Kim" }, {2, "Thủy"}, {3, "Hỏa"}, {4,"Thổ"}, {5, "Mộc"}
        };
    }
}

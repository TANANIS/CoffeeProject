using Microsoft.AspNetCore.Mvc;

namespace big.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
        

        // 研磨粗細、萃取係數、注水方式 計算結果方法
        public JsonResult Grind_Size(string roast_level)
        {
            string grind_size;
            string extraction_conditions;
            string add_water;

            switch (roast_level)
            {
                case "淺焙":
                    grind_size = "精細研磨，研磨最細（如味精顆粒大小）";
                    extraction_conditions = "通常需要較長的萃取時間，因為需要更多時間來萃取酸味和香氣";
                    add_water = "建議三段注水：首先注水悶蒸，讓咖啡粉充分濕潤。之後按均勻的節奏分兩次注水，每次讓水位慢慢下降再注入新水";
                    break;
                case "中淺焙":
                    grind_size = "細研磨，研磨稍細";
                    extraction_conditions = "萃取時間比淺焙略短，能保持均衡的酸甜感";
                    add_water = "建議三段注水：注水悶蒸完，然後以均勻速度注入第二段水，當水位下降至粉層表面時，進行第三段注水，保持水流輕柔，避免過度攪動粉層";
                    break;
                case "中焙":
                    grind_size = "中細研磨，研磨中等（如海鹽粒大小）";
                    extraction_conditions = "萃取時間適中，可以同時平衡酸味和苦味";
                    add_water = "建議兩段注水：先進行悶蒸，之後以均勻速度將水分兩段注入，可稍微提高水位來增加萃取率，但也要避免過多攪動粉層";
                    break;
                case "中深焙":
                    grind_size = "中研磨，研磨稍粗";
                    extraction_conditions = "萃取時間不宜過長，以保留其甜感和微微的苦味";
                    add_water = "建議兩段注水：悶蒸後開始均勻注水，第二段注水量略少於第一段，以達到合適的濃度。注意保持水流穩定，以避免過度萃取苦味";
                    break;
                case "深焙":
                    grind_size = "粗研磨，研磨較粗（如芝麻顆粒大小）";
                    extraction_conditions = "通常不需要太長的萃取時間，以避免過度苦澀";
                    add_water = "建議單段注水：悶蒸完成後，緩慢均勻地注入所有水量，保持水流細而穩定。避免多段注水，以減少苦澀感並保持順滑的口感。可以稍微加快注水速度，注意不要過度萃取";
                    break;
                default:
                    grind_size = "錯誤錯誤";
                    extraction_conditions = "錯誤錯誤";
                    add_water = "錯誤錯誤";
                    break;
            }
            // 返回JSON格式的研磨粗細
            return Json(new { grindsize = grind_size, extractionconditions = extraction_conditions, addwater = add_water });
        }



    }
}

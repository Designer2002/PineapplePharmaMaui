using System.Text.Json.Serialization;

namespace winui_cooler;
public class ShoppingCart
{
    [JsonPropertyName("Content")]
    public List<MedicineShoppingCartView> Content { get; set; } = new List<MedicineShoppingCartView>();

    [JsonPropertyName("UserEmail")]
    public string UserEmail { get; set; }

    [JsonPropertyName("User")]
    public object User { get; set; }
}
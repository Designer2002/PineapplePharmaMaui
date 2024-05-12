namespace winui_cooler;

public class Medicine
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string ActiveComponent { get; set; }
    public string Name { get; set; }        
    public string Description { get; set; }
    public string ReleaseForm { get; set; }
    public string Distributer { get; set; }
    public int Expiration { get; set; }
    public double Price { get; set; }
    public bool Prescription { get; set; }

}

public class MedicineView
{
    public int Name {get; set;}
    public int Price {get; set; }
}

public class MedicineShoppingCartView
{
    public int Id { get; set; }
    public int Count { get; set; }
}


namespace winui_cooler;
public class MedicineManager
{
    private static Random tmp = new Random();
    public static Category Current {get; set; }
    public static Medicine ChosenMedicine { get; set; } = temporary;
    private static List<string> descr = new List<string>()
    {
        "Anti-cold medicines, such as decongestants and antihistamines, alleviate symptoms like congestion, runny nose, and sneezing. They work by reducing inflammation and drying up nasal passages. However, they may cause drowsiness or other side effects, so it's essential to use them cautiously and follow dosage instructions.",
        "ENT (Ear, Nose, Throat) medicine focuses on diagnosing and treating disorders of the ears, nose, throat, and related structures. Specialists in this field address issues like sinus infections, hearing loss, tonsillitis, and vocal cord disorders. Treatment may involve medication, surgery, or other therapies tailored to the specific condition. Regular check-ups with an ENT specialist can help maintain overall ear, nose, and throat health.",
        "Gastroenterology medicine specializes in diagnosing and treating disorders of the digestive system, including the esophagus, stomach, intestines, liver, and pancreas. Gastroenterologists address conditions like acid reflux, irritable bowel syndrome, liver disease, and gastrointestinal cancers. Treatment options range from lifestyle changes and medication to endoscopic procedures and surgery, tailored to each patient's needs. Regular screenings and consultations with a gastroenterologist are crucial for maintaining digestive health.",
        "Dental medical items encompass a wide range of products used for oral health care. These include toothbrushes, toothpaste, dental floss, mouthwash, and specialized tools for cleaning teeth and gums. Regular use of these items helps prevent tooth decay, gum disease, and other oral health issues, promoting overall dental hygiene and wellbeing.",
        "Bone health medical items encompass supplements like calcium and vitamin D, prescribed to prevent osteoporosis and maintain bone density. Additionally, medications like bisphosphonates and hormone therapy may be prescribed to treat osteoporosis. Weight-bearing exercise and a balanced diet also play crucial roles in maintaining bone health. Regular check-ups and bone density scans help monitor bone health status.",
        "Eye vitamins and drugs play essential roles in maintaining eye health. Supplements containing antioxidants like vitamins A, C, and E, as well as minerals like zinc and selenium, can support eye function and reduce the risk of age-related macular degeneration and cataracts. Medications such as eye drops and ointments treat conditions like glaucoma, dry eye syndrome, and eye infections, improving vision and comfort. Regular eye exams help monitor eye health and determine the need for specific vitamins or medications.",
        "First aid items are essential for providing immediate care in emergencies. They include bandages, antiseptics, gauze pads, adhesive tape, and scissors, used to clean and dress wounds. Additionally, items like splints, cold packs, and CPR masks assist in stabilizing injuries and managing medical crises until professional help arrives. Regular replenishment and training in first aid procedures ensure preparedness for unexpected situations.",
        "Women's accessories for gynecology, such as tampons and menstrual cups, provide menstrual hygiene management. These products offer comfort and convenience during menstruation, absorbing menstrual flow and preventing leaks. Additionally, vaginal moisturizers and lubricants help alleviate discomfort associated with dryness and improve sexual health. Regular use of these accessories promotes women's reproductive health and well-being.",
        "Veterinary medicine encompasses healthcare for animals, addressing a wide range of conditions from injuries to diseases. Veterinarians diagnose illnesses, prescribe medications, and perform surgeries to ensure the well-being of pets, livestock, and wildlife. Preventative care, vaccinations, and parasite control are also crucial aspects, promoting animal health and welfare.",
        "Baby accessories like diapers (such as Pampers) and feeding bottles are essential for infant care. Diapers keep babies dry and comfortable, while feeding bottles provide nourishment for infants. Additionally, pacifiers soothe babies and aid in calming them. These accessories contribute to the overall health and well-being of babies, ensuring their comfort and nutrition.",
        "Spa goods available in drugstores include bath salts, facial masks, and aromatherapy oils, offering relaxation and rejuvenation at home. These products provide a spa-like experience, promoting stress relief, skincare, and overall well-being. Easy accessibility in drugstores allows customers to create personalized spa treatments tailored to their preferences and needs.",
        "Contraception available in drugstores includes various options like condoms, birth control pills, and emergency contraception. These products offer individuals control over their reproductive health, preventing unintended pregnancies. Easy access to contraception in drugstores promotes safe and responsible sexual practices, empowering individuals to make informed choices about their sexual health."
    };
    public static Dictionary<string, Category> categories = new Dictionary<string, Category>()
    {
        {"f", new Category() {Name = "Fever Medicine", ImagePath = "fever", Index = 0, Description=descr[0]}},
        { "e", new Category() {Name = "ENT Medicine", ImagePath = "ent", Index = 1, Description=descr[1]}},
        { "g", new Category() {Name = "Gastroenterology Medicine", ImagePath = "toi", Index = 2, Description=descr[2]}},
        { "d", new Category() {Name = "Dental Medicine", ImagePath = "croc", Index = 3, Description=descr[3]}},
        { "b", new Category() {Name = "Bone Health Medicine", ImagePath = "bones", Index = 4, Description=descr[4]}},
        { "o", new Category() {Name = "Ophthalmology Medicine", ImagePath = "opht", Index = 5, Description=descr[5]}},
        { "fa", new Category() {Name = "First Aid Medicine", ImagePath = "faid", Index = 6, Description=descr[6]}},
        { "w", new Category() {Name = "Woman Health Accesoires", ImagePath = "woman", Index = 7, Description=descr[7]}},
        { "v", new Category() {Name = "Veterinary Medicine", ImagePath = "vet", Index = 8, Description=descr[8]}},
        { "cc", new Category() {Name = "Child Care Accessoires", ImagePath = "baby", Index = 9, Description=descr[9]}},
        { "s", new Category() {Name = "SPA", ImagePath = "spa", Index = 10, Description=descr[10]}},
        { "c", new Category() {Name = "Contraception", ImagePath = "cont", Index = 11, Description=descr[11]}}
    };

    public async static Task<int> CategoryCountAsync(string category)
    {
        var medicines = await CategoryMedicines(category);
        return medicines.Count;
    }

    public static Medicine temporary = new Medicine { Id = 0, Category = categories["b"].Name, ActiveComponent = "Denosumab", Name = "Prolia", Description = "Prolia is a monoclonal antibody medication used to treat osteoporosis in postmenopausal women and men at high risk of fractures. It helps increase bone density and reduce the risk of fractures by inhibiting bone resorption.", Distributer = "Japan", Expiration = 1, Price = 200, ReleaseForm = "Injection", Prescription = false};
    public static async Task<List<Medicine>> CategoryMedicines(string category)
    {
        return await ConnectionManager.GetCategoryList(category);
    }
    public static List<Medicine> CategoryMedicinesTmp = new List<Medicine>() { temporary };
    // public static async Task<Medicine> GetByIdAsync(int id)
    // {
    //     var medicines = await CategoryMedicines();
    //     return medicines.Where(i => i.Id == id).First();
    // }

    public Task<ShoppingCart> ShoppingCart => ConnectionManager.GetShoppingCart();

    public static Medicine MedicineToDelete { get; set; }
}
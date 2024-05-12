namespace winui_cooler;

public partial class Shop : ContentPage
{
    private Color dirtyYellow = MyColors.dirtyYellow;
	private Color darkGray = MyColors.darkGray;
    public Shop()
	{
		InitializeComponent();
	}
    private async void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        await Navigation.PushAsync(new StartPage());
    }
    private void OnCartTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        Navigation.PushAsync(new CartPage());
    }
    private async void OnNewsLabelTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        MainPage.isOnNews = true;
        var mp = new MainPage();
        await Navigation.PushAsync(mp);
        mp.ScrollToNewsLabel();
    }
    private void OnShopLabelTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        Navigation.PushAsync(new Shop());
    }
    private async void OnLabelTapped(object sender, TappedEventArgs e)
    {
        await Animations.TextColorBlink(sender as Label, darkGray, dirtyYellow);
    }
    private void OnPersonTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
    }
    private void OnHomeTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        Navigation.PushAsync(new MainPage());
    }
    private async void OnGlyphTapped(object sender, TappedEventArgs e)
    {
        await Animations.GlyphColorBlink(sender as Image, darkGray, dirtyYellow);
    }
    private async void OnBtnTapped(object sender, TappedEventArgs e)
    {
        await Animations.ButtonBorderColorBlink(sender as Border, Colors.Black, Colors.Aqua);
        await Navigation.PushAsync(new CategoryPage());
    }

    void OnFeverEnter(object sender, PointerEventArgs e)
    {
        fever_img.Source = ImageSource.FromFile("fever.gif");
    }

    void OnFeverExit(object sender, PointerEventArgs e)
    {
        fever_img.Source = ImageSource.FromFile("fever_s.jpg");
    }

    void OnENTEnter(object sender, PointerEventArgs e)
    {
        ent_img.Source = ImageSource.FromFile("ent.gif");
    }

    void OnENTExit(object sender, PointerEventArgs e)
    {
        ent_img.Source = ImageSource.FromFile("ent_s.jpg");
    }

    void OnGastEnter(object sender, PointerEventArgs e)
    {
        gast_img.Source = ImageSource.FromFile("stomach.gif");
    }

    void OnGastExit(object sender, PointerEventArgs e)
    {
        gast_img.Source = ImageSource.FromFile("stomach_s.jpg");
    }

    void OnDentEnter(object sender, PointerEventArgs e)
    {
        dent_img.Source = ImageSource.FromFile("tooth.gif");
    }

    void OnDentExit(object sender, PointerEventArgs e)
    {
        dent_img.Source = ImageSource.FromFile("tooth_s.jpg");
    }

    void OnBoneEnter(object sender, PointerEventArgs e)
    {
        bone_img.Source = ImageSource.FromFile("bone.gif");
    }

    void OnBoneExit(object sender, PointerEventArgs e)
    {
        bone_img.Source = ImageSource.FromFile("bone_s.jpg");
    }

    void OnFirstAidEnter(object sender, PointerEventArgs e)
    {
        fa_img.Source = ImageSource.FromFile("firstaid.gif");
    }

    void OnFirstAidExit(object sender, PointerEventArgs e)
    {
        fa_img.Source = ImageSource.FromFile("firstaid_s.jpg");
    }

    void OnEyeEnter(object sender, PointerEventArgs e)
    {
        eye_img.Source = ImageSource.FromFile("eye.gif");
    }

    void OnEyeExit(object sender, PointerEventArgs e)
    {
        eye_img.Source = ImageSource.FromFile("eye_s.jpg");
    }

     void OnWomanEnter(object sender, PointerEventArgs e)
    {
        wom_img.Source = ImageSource.FromFile("woman.gif");
    }

    void OnWomanExit(object sender, PointerEventArgs e)
    {
        wom_img.Source = ImageSource.FromFile("woman_s.jpg");
    }

     void OnVetEnter(object sender, PointerEventArgs e)
    {
        vet_img.Source = ImageSource.FromFile("paw.gif");
    }

    void OnVetExit(object sender, PointerEventArgs e)
    {
        vet_img.Source = ImageSource.FromFile("paw_s.jpg");
    }

     void OnChildEnter(object sender, PointerEventArgs e)
    {
        baby_img.Source = ImageSource.FromFile("baby.gif");
    }

    void OnChildExit(object sender, PointerEventArgs e)
    {
        baby_img.Source = ImageSource.FromFile("baby_s.jpg");
    }

    void OnSpaEnter(object sender, PointerEventArgs e)
    {
        spa_img.Source = ImageSource.FromFile("spa.gif");
    }

    void OnSpaExit(object sender, PointerEventArgs e)
    {
        spa_img.Source = ImageSource.FromFile("spa_s.jpg");
    }
    void OnContEnter(object sender, PointerEventArgs e)
    {
        lip_img.Source = ImageSource.FromFile("lips.gif");
    }

    void OnContExit(object sender, PointerEventArgs e)
    {
        lip_img.Source = ImageSource.FromFile("lips_s.jpg");
    }
    



    private void OnBtnTappedF(object sender, TappedEventArgs e)
    {
       MedicineManager.Current = MedicineManager.categories["f"];
       OnBtnTapped(sender, e);
    }
    private void OnBtnTappedE(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["e"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedG(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["g"];
        OnBtnTapped(sender, e);
    }

     private void OnBtnTappedV(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["v"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedS(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["s"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedD(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["d"];
        OnBtnTapped(sender, e);
    }

     private void OnBtnTappedW(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["w"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedC(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["c"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedB(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["b"];
        OnBtnTapped(sender, e);
    }

     private void OnBtnTappedFA(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["fa"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedCC(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["cc"];
        OnBtnTapped(sender, e);
    }

    private void OnBtnTappedO(object sender, TappedEventArgs e)
    {
        MedicineManager.Current = MedicineManager.categories["o"];
        OnBtnTapped(sender, e);
    }
}
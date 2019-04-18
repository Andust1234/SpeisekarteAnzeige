public struct Speise
{
    public int ID { get; set; }
    public string Titel { get; set; }
    public BildRawData Bild { get; set; }
    public string Preis { get; set; }
    public string Beschreibung { get; set; }
    public string SpeisenArt { get; set; }
    public int SpeisenArt_ID { get; set; }

}

public struct BildRawData
{
    public byte[] BildRaw { get; set; }
    public int BildWidth { get; set; }
    public int BildHight { get; set; }
} 

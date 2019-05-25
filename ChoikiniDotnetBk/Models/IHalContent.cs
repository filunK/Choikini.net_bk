namespace ChoikiniDotnetBk.Models
{
    /*
     * HALに引き渡すデータの基本モデル
     */
    public interface IHalContent
    {
        string SelfUri{get;}

        IEmbeddedModel Embedded{get;}
    }
}
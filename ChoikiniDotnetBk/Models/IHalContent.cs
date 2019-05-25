namespace ChoikiniDotnetBk.Models
{
    /*
     * HALに引き渡すデータの基本モデル
     */
    public interface IHalContent
    {
        string SelfUri{get;}

        ResponseState State{get;}

        string StateDetail{get;}

        object [] Response{get;}

        IEmbeddedModel[] Embedded{get;}
    }
}
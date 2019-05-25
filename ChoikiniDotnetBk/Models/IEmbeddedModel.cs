namespace ChoikiniDotnetBk.Models
{
    public interface IEmbeddedModel
    {
        ResponseState state{get;}

        string StateDetail{get;}

        object [] Response{get;}

    }
}
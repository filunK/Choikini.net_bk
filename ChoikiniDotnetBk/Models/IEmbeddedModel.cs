namespace ChoikiniDotnetBk.Models
{
    public interface IEmbeddedModel
    {
        string Name{get;}

        IEmbeddedContent[] EmbeddedContent{get;}

    }
}
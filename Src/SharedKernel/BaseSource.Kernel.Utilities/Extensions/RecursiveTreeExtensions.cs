namespace BaseSource.Kernel.Utilities.Extensions;

public static class RecursiveTreeExtensions
{
    public static List<TTree> RecursiveTree<TTree>(this List<TTree> list)
        where TTree : class, ITreeViewModel<TTree>
    {
        list.ForEach(r => r.Children = list.Where(ch => ch.ParentId == r.Id).ToList());//Cast<ITreeViewModel<TTree>>()

        return list.Where(i => i.ParentId == null).ToList();
    }
}

public interface ITreeViewModel<T>
    where T : class
{
    int Id { get; set; }
    int? ParentId { get; set; }
    List<T> Children { get; set; }
}

public abstract class TreeViewModel<T> : ITreeViewModel<T>
    where T : class
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public required List<T> Children { get; set; }
}

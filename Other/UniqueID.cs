
/// <summary>
/// Унікальний ідентифікатор
/// </summary>
public class UniqueID
{
    /// <summary>
    /// Пустий вказівник
    /// </summary>
    public UniqueID() { }

    /// <summary>
    /// Унікальний ідентифікатор
    /// </summary>
    /// <param name="uGuid">Унікальний ідентифікатор</param>
    public UniqueID(Guid uGuid)
    {
        UGuid = uGuid;
    }

    /// <summary>
    /// Унікальний ідентифікатор
    /// </summary>
    /// <param name="uGuid">Унікальний ідентифікатор як object</param>
    public UniqueID(object? uGuid)
    {
        if (uGuid != null && uGuid != DBNull.Value && uGuid is Guid guid)
            UGuid = guid;
        else
            UGuid = Guid.Empty;
    }

    /// <summary>
    /// Унікальний ідентифікатор у формі тексту. Використовується Guid.Parse(uGuid).
    /// </summary>
    /// <param name="uGuid">Унікальний ідентифікатор</param>
    public UniqueID(string uGuid)
    {
        if (Guid.TryParse(uGuid, out Guid result))
            UGuid = result;
        else
            UGuid = Guid.Empty;
    }

    /// <summary>
    /// Чи це пустий вказівник?
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return UGuid == Guid.Empty;
    }

    /// <summary>
    /// Опустошити, обнулити
    /// </summary>
    public void Clear()
    {
        UGuid = Guid.Empty;
    }

    /// <summary>
    /// Згенерувати новий ідентифікатор
    /// </summary>
    public void New()
    {
        UGuid = Guid.NewGuid();
    }

    /// <summary>
    /// Новий UniqueID
    /// </summary>
    public static UniqueID NewUnigueID()
    {
        return new UniqueID(Guid.NewGuid());
    }

    public static UniqueID NewEmpty()
    {
        return new UniqueID(Guid.Empty);
    }

    /// <summary>
    /// Пустий ідентифікатор
    /// </summary>
    public static readonly Guid Empty = Guid.Empty;

    /// <summary>
    /// Унікальний ідентифікатор
    /// </summary>
    public Guid UGuid { get; private set; } = Guid.Empty;

    /// <summary>
    /// Порівняння
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj != null && UGuid == ((UniqueID)obj).UGuid;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UGuid);
    }

    /// <summary>
    /// ToString
    /// </summary>
    public override string ToString()
    {
        return UGuid.ToString();
    }
}
namespace BrewUp.Shared.Entities;

public abstract class EntityBase : IEquatable<EntityBase>
{
    public string Id { get; set; } = string.Empty;

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EntityBase);
    }

    public bool Equals(EntityBase? other)
    {
        return null != other && GetType() == other.GetType() && other.Id == Id;
    }

    public static bool operator ==(EntityBase? entity1, EntityBase? entity2)
    {
        if ((object)entity1 == null && (object)entity2 == null)
            return true;
        if ((object)entity1 == null || (object)entity2 == null)
            return false;

        return entity1.GetType() == entity2.GetType() && entity1.Id == entity2.Id;
    }

    public static bool operator !=(EntityBase? entity1, EntityBase? entity2)
    {
        return !(entity1 == entity2);
    }
}
public class Bone
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public List<Bone> Children { get; set; }

    public Bone()
    {
        Position = Vector2.Zero;
        Rotation = 0f;
        Children = new List<Bone>();
    }
}

public class Skeleton
{
    public Bone RootBone { get; set; }

    public Skeleton()
    {
        RootBone = new Bone();
    }
}

public class Player
{
    public Skeleton Skeleton { get; set; }
    // Other player properties...

    public Player()
    {
        Skeleton = new Skeleton();
        // Initialize other player properties...
    }

      public void Update(GameTime gameTime)
    {
        // Example: Update bone positions and rotations based on animations or input
        UpdateSkeleton(Skeleton.RootBone, Vector2.Zero, 0f); // Pass appropriate parameters
    }

    // Recursively update bone positions and rotations
    private void UpdateSkeleton(Bone bone, Vector2 parentPosition, float parentRotation)
    {
        // Calculate bone position and rotation relative to its parent
        bone.Position = RotateVector(Vector2.Zero, bone.Position, parentRotation) + parentPosition;
        bone.Rotation += parentRotation;

        foreach (Bone childBone in bone.Children)
        {
            UpdateSkeleton(childBone, bone.Position, bone.Rotation);
        }
    }
	
	 // Helper method to rotate a vector
    private Vector2 RotateVector(Vector2 origin, Vector2 point, float angle)
    {
        float rotatedX = (float)(Math.Cos(angle) * (point.X - origin.X) - Math.Sin(angle) * (point.Y - origin.Y) + origin.X);
        float rotatedY = (float)(Math.Sin(angle) * (point.X - origin.X) + Math.Cos(angle) * (point.Y - origin.Y) + origin.Y);
        return new Vector2(rotatedX, rotatedY);
    }


     // Recursively draw the player's model based on bone positions and rotations
    private void DrawSkeleton(SpriteBatch spriteBatch, Bone bone)
    {
        // Example: Draw sprites or textures based on bone positions and rotations using spriteBatch
        // Use bone.Position and bone.Rotation to render parts of the player model
        
        // Draw current bone here...

        foreach (Bone childBone in bone.Children)
        {
            DrawSkeleton(spriteBatch, childBone);
        }
    }
}

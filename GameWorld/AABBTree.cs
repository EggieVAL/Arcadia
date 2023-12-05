using Arcadia.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcadia.GameWorld
{
    public struct AABB
    {
        public Vector2 tr;
        public Vector2 bl;

        public AABB(Vector2 tr, Vector2 bl)
        {
            this.tr = tr;
            this.bl = bl;
        }

        public AABB Union(AABB other)
        {
            return new AABB(Vector2.Max(tr, other.tr), Vector2.Min(bl, other.bl));
        }

        public float Area()
        {
            return (tr.X - bl.X) * (tr.Y - bl.Y);
        }
    }
    public class Node
    {
        public AABBTree tree;
        public int objectIndex;
        public Node parent;
        public Node left;
        public Node right;
        
        public AABB box;

        public Node(AABBTree tree, int obj_i)
        {
            this.tree = tree;
            this.objectIndex = obj_i;
        }

        public Node(AABBTree tree, int obj_i, AABB box)
        {
            this.tree = tree;
            this.objectIndex = obj_i;
            this.box = box;
        }

        public Node(AABBTree tree, int obj_i, Node parent)
        {
            this.tree = tree;
            this.objectIndex = obj_i;
        }
        public Node(AABBTree tree, int obj_i, Node parent, AABB box)
        {
            this.tree = tree;
            this.objectIndex = obj_i;
            this.box = box;
        }
    }
    public class AABBTree
    {
        public Node root;
        public int count;
        public World world;
        public bool IsOverlapping(AABB box, Ray ray)
        {
            return false;
        }

        public float ComputeCost(Node parent, Node child)
        {
            float cost = 0.0f;
            Node r = parent;
            cost += r.box.Union(child.box).Area();
            while (r.parent != null)
            {
                r = r.parent;
                cost += r.box.Union(child.box).Area() - child.box.Area();
            }
            
        }

        public Node FindBestSibling(Node n)
        {
            Node best = null;
            float bcost = -1;
            PriorityQueue<Node,float> q = new PriorityQueue<Node,float>();
            q.Enqueue(root, 0);
            
            while(q.Count > 0){
                Node tmp = q.Dequeue();
                float tcost = ComputeCost(tmp,n);
                if (tcost < bcost)
                {
                    best = tmp;
                    bcost = tcost;
                }
                float lcost = ComputeCost(n, n);
                if(lcost < bcost)
                {
                    q.Enqueue(tmp.left, ComputeCost(tmp.left, n));
                    q.Enqueue(tmp.right, ComputeCost(tmp.right, n));
                }
            }
            return best;
        }


        public void Rotate(Node n)
        {
            
        }

        public void InsertLeaf(int obj_i, AABB box)
        {
            if (count == 0)
            {
                count = 1;
                root = new Node(this, obj_i, box);
                return;
            }
            Node newNode = new Node(this, obj_i, box);
            Node sibling = FindBestSibling(newNode);

            Node oldParent = sibling.parent;
            Node newParent = new Node(this, -1, oldParent);
            newParent.box = sibling.box.Union(box);
            if(oldParent != null)
            {
                if(oldParent.left == sibling)
                {
                    oldParent.left = newParent;
                }
                else
                {
                    oldParent.right = newParent;
                }
                newParent.left = sibling;
                sibling.parent = newParent;
                newParent.right = newNode;
                newNode.parent = newParent;
            }
            else
            {
                newParent.left = sibling;
                sibling.parent = newParent;
                newParent.right = newNode;
                newNode.parent = newParent;
            }

            Node n = newParent;
            while(n != root)
            {
                Node c1 = n.left;
                Node c2 = n.right;
                n.box = c1.box.Union(c2.box);

                Rotate(n);

                n = n.parent;
            }
        }

        public bool TreeCollision(RenderableObject entity)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count == 0)
            {
                Node n = stack.Pop();
                
            }
        }
    }
    

    
}

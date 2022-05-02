using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    /// <summary>
    /// 1)This class describes a tree node
    /// 2)Assumes a single parent
    /// 3)It can be extended to add new functionality
    /// 4)It is type safe
    /// 5)It's methods cannot be extended because it's not virtual
    /// 6)If we want to add information, we can do it either by subclassing or by 
    /// incorporating a generic type
    /// 7)If we want to add labeling, we can add a string or a type generating labels
    /// 
    /// 1)We want to investigate what are the benefits of using interfaces 
    /// for the node classes 
    /// a)Remove dependencies. 
    /// b)Improves encapsulation
    /// c)An interface can accept any class implementing the interface (typesafety concerns
    /// can be solved using type paramaters(??generics??)) 
    /// d)Interfaces can support multiple inheritence 
    /// 2)We want to investigate wether it is a possibilty to unite these three implementations
    /// so as to create a prototype class that would work for all three cases
    /// </summary>
    /// 


    interface CreateDiGraphTypes    //Method that creates a directed graph
    {
        void DGRepresentation(HyperGraphNode root);
    }

    interface CreateTreeTypes   //Method that creates a tree
    {
        void TreeRepresentation(HyperGraphNode root);

    }
    interface CreateUDiGraphTypes   //Method that creates an undirected graph
    {
        void UGRepresentation(HyperGraphNode root);
    }

    public class HyperGraphNode
    {
        public ArrayList m_neighbours = new ArrayList();
        public int index;
        public int context;
        public HyperGraphNode() { }
        public HyperGraphNode(int context) {
            this.context = context;
        }


        public void addNeighbour(HyperGraphNode child, int context) {
            child.context = context;
            m_neighbours.Add(child);
            
        }

        public void removeNeighbour(HyperGraphNode child, int context) {
            m_neighbours.Remove(child);
        }

        public bool hasNeighbour(HyperGraphNode child)
        {
            
            if (child.m_neighbours==null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /*public HyperGraphNode getNeighbours(int index, int context)
        {
            return this.m_neighbours[context][index];
        }

        public int getNumberOfNeighbours(int context)
        {
            return this.m_neighbours[context].Count;
        }*/
    }
    
    public class TreeNode {     //methods for tree node
    public List<TreeNode> m_children = new List<TreeNode>();
    public int index;
    protected string inf;
        
    public void addChild(TreeNode child) {
        m_children.Add(child);
    }

    public void removeChild(TreeNode child) {
        m_children.Remove(child);
    }

    public Boolean hasChild(TreeNode child) {
        bool isEmpty = !m_children.Any();
        if (isEmpty) {
            return false;
        } else {
            return true;
        }
    }

    public TreeNode getChild(int index) {
        return this.m_children[index];
    }

    public int getNumberOfChildren() {
        bool isEmpty = !m_children.Any();
        if (isEmpty) {
            return 0;
        }
        else {
            return m_children.Count;
        }
                

    }
}

    public class DiGraphNode {      //methods for directed graph node 
        public List<DiGraphNode> m_descendant = new List<DiGraphNode>();
        public List<DiGraphNode> m_ancestor = new List<DiGraphNode>();
        public string label;
        public int index;
        public DiGraphNode() { }
        public DiGraphNode(int index)
        {
            this.index = index;
        }


        public virtual void addDescendant(DiGraphNode child) {
            m_descendant.Add(child);
        }

        public void removeDescendant(DiGraphNode child) {
            m_descendant.Remove(child);

        }

        public bool hasDescendant(DiGraphNode child) {
            bool isEmpty = !m_descendant.Any();
            if (isEmpty) {
                return false;
            } else {
                return true;
            }
        }

        public DiGraphNode getDescendants(int index) {
            return this.m_descendant[index];
        }

        public int getNumberOfDescendants() {
            bool isEmpty = !m_descendant.Any();
            if (isEmpty) {
                return 0;
            } else {
                return m_descendant.Count;
            }
        }
        public void addAncestor(DiGraphNode child) {
            m_ancestor.Add(child);
        }

        public void removeAncestor(DiGraphNode child) {
            m_ancestor.Remove(child);
        }

        public bool hasAncestor(DiGraphNode child) {
            bool isEmpty = !m_ancestor.Any();
            if (isEmpty) {
                return false;
            } else {
                return true;
            }
        }

        public DiGraphNode getAncestor(int index) {
            return this.m_ancestor[index];
        }

        public int getNumberOfAncestors() {
            bool isEmpty = !m_ancestor.Any();
            if (isEmpty) {
                return 0;
            } else {
                return m_ancestor.Count;
            }
        }

    
    }

    public class UDiGraphNode 
    {     //methods for undirected graph node
        public List<UDiGraphNode> m_neighbours = new List<UDiGraphNode>();
        public int index;
        public void addNeighbour(UDiGraphNode child) {
            m_neighbours.Add(child);
        }

        public void removeNeighbour(UDiGraphNode child) {
            m_neighbours.Remove(child);
        }

        public Boolean hasNeighbour(UDiGraphNode child) {
            bool isEmpty = !m_neighbours.Any();
            if (isEmpty) {
                return false;
            } else {
                return true;
            }
        }

        public UDiGraphNode getNeighbours(int index) {
            return this.m_neighbours[index];
        }

        public int getNumberOfNeighbours() {
            bool isEmpty = !m_neighbours.Any();
            if (isEmpty) {
                return 0;
            } else {
                return m_neighbours.Count;
            }
        }
    }

    public class Tree : TreeNode    //Tree graph generation and Graphviz implementation
    {
        TreeNode root;
        public StreamWriter m_dotFile = new StreamWriter("tree.dot");   //Create graph .dot file
        public Boolean isFirst = false;
        TreeNode temp;
        public Tree(TreeNode root) {
            this.root = root;
            temp = root;
            m_dotFile.WriteLine("digraph G{");
        }
        public virtual void TreeRepresentation(TreeNode root) {  //Recursive method that uses foreach to traverse every node and create the .dot file for GraphViz
            Console.WriteLine(root.index);
            if (root.hasChild(root)==true && isFirst == false)
                m_dotFile.Write("\""+root.index+"\"->\"");  //Write in .dot file

            foreach (TreeNode node in root.m_children) {
                if (root.hasChild(root) == true && isFirst == false)
                    m_dotFile.Write(node.index + "\";\n");  //Write in .dot file
                else if (node.hasChild(node) == true) {
                    m_dotFile.Write("\"" + temp.index + "\"->\"" + node.index + "\";\n");   //Write in .dot file
                }
                if (node.hasChild(node) == false) {
                    isFirst = true;
                    Console.WriteLine("Was first when node was " + node.index);
                } 
                else {
                    isFirst = false;
                    Console.WriteLine("Was NOT first when node was " + node.index);
                }

                TreeRepresentation(node);

            }
        }


        public int CloseFile() {    //.dot file closing method.
            m_dotFile.WriteLine("}");
            m_dotFile.Close();

            // Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " + Path.GetFileName("tree.dot") + " -o " + Path.GetFileNameWithoutExtension("tree") + ".gif";
            //Enter the executable to run, including the complete path
            start.FileName = "dot";
            //Do you want to show a console window
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start)) {
                proc.WaitForExit();
            // Retrieve the app's exit process
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        
      
    }

    class UGraph : UDiGraphNode     //Undirected graph generation and traversing class
    {
        UDiGraphNode root;
        public StreamWriter m_dotFile = new StreamWriter("Undirected_Graph.dot");   //Create graph .dot file
        public Boolean isFirst = false;
        bool[] isVisited;
        public int c = 0;
        UDiGraphNode temp;
        DiGraphNode node0 = new DiGraphNode(0);
        DiGraphNode temp2;
        List<DiGraphNode> dg = new List<DiGraphNode>();
        public UGraph(UDiGraphNode root, bool[] isVisited) {
            this.root = root;
            temp = root;
            this.isVisited = isVisited;
            m_dotFile.WriteLine("graph G{");  
        }


        int i = 0;
        
        public virtual void UGRepresentation(UDiGraphNode root)     //Recursive method that uses foreach to traverse every node and create the .dot file for GraphViz
        {
            if (i == 0)         //Initialization mechanism used for safety if the isVisited bool array is not by default initialized to false. Runs only once
            {
                for (int j = 0; j < 5; j++)
                {

                    isVisited[j] = false;
                }

            }
            i++;

            foreach (UDiGraphNode node in root.m_neighbours)    //Point of recursion
            {

                if (isVisited[node.index] == false && c < 2)
                {
                    m_dotFile.Write("\"" + root.index + "\"--\"");  //Write in .dot file
                    m_dotFile.Write(node.index + "\";\n");  //Write in .dot file
                    Console.WriteLine("node is " + node.index);
                    isVisited[root.index] = true;
                    UGRepresentation(node);
                }
                else if (isVisited[node.index] == true)
                {
                    m_dotFile.Write("\"" + root.index + "\"--\"");  //Write in .dot file
                    m_dotFile.Write(node.index + "\";\n");  //Write in .dot file
                    Console.WriteLine("node is " + node.index);
                    ++c;
                }


            }
            
        }

        public int CloseFile() {    //.dot file closing method.
            m_dotFile.WriteLine("}");
            m_dotFile.Close();

            // Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " + Path.GetFileName("Undirected_Graph.dot") + " -o " + Path.GetFileNameWithoutExtension("Undirected_Graph") + ".gif";
            //Enter the executable to run, including the complete path
            start.FileName = "dot";
            //Do you want to show a console window
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start)) {
                proc.WaitForExit();
                // Retrieve the app's exit process
                exitCode = proc.ExitCode;
            }

            return 0;
        }
    }

    public class DGraph : DiGraphNode   //Directed graph generation and traversing class
    {         
        DiGraphNode root;
        public StreamWriter m_dotFile = new StreamWriter("Directed_Graph.dot"); //Create graph .dot file
        public Boolean isFirst = false;
        bool[] isVisited;
        public int c = 0;
        DiGraphNode temp;

        public DGraph(DiGraphNode root, bool[] isVisited) {
            this.root = root;
            temp = root;
            this.isVisited = isVisited;
            m_dotFile.WriteLine("digraph G{");
        }

        int i = 0;
        public void DGRepresentation(DiGraphNode root) {

            for (int j = 0; j < 5; j++)     //Initialization mechanism used for safety if the isVisited bool array is not by default initialized to false. Runs only once
            {
                if (i == 0)
                    isVisited[j] = false;
            }
            i++;

            foreach (DiGraphNode node in root.m_descendant) //Recursion
            {
                if (isVisited[node.index] == false && c < 2)    
                {
                    m_dotFile.Write("\"" + root.index + "\"->\"");  //Write in .dot file
                    m_dotFile.Write(node.index + "\";\n");  //Write in .dot file
                    Console.WriteLine("node is " + node.index);
                    isVisited[root.index] = true;
                    DGRepresentation(node);
                }
                else if (isVisited[node.index] == true)
                {
                    m_dotFile.Write("\"" + root.index + "\"->\"");  //Write in .dot file
                    m_dotFile.Write(node.index + "\";\n");  //Write in .dot file
                    Console.WriteLine("node is " + node.index);
                    ++c;
                }


            }
        }
        public int CloseFile()  //.dot file closing method.
        {
            m_dotFile.WriteLine("}");
            m_dotFile.Close();

            // Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " + Path.GetFileName("Directed_Graph.dot") + " -o " + Path.GetFileNameWithoutExtension("Directed_Graph") + ".gif";
            //Enter the executable to run, including the complete path
            start.FileName = "dot";
            //Do you want to show a console window
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start)) {
                proc.WaitForExit();
                // Retrieve the app's exit process
                exitCode = proc.ExitCode;
            }

            return 0;
        }
    }

    public class HGraph : HyperGraphNode, CreateDiGraphTypes, CreateTreeTypes, CreateUDiGraphTypes
    {
        HyperGraphNode root;
        public StreamWriter m_dotFile = new StreamWriter("Hyper_Graph.dot");    //Create graph .dot file
        public Boolean isFirst = false;
        bool[] isVisited;
        public int c = 0;
        HyperGraphNode temp;

        public HGraph(HyperGraphNode root, bool[] isVisited) //constructor used for initialization of some values
        {
            this.root = root;
            temp = root;
            this.isVisited = isVisited;
            
        }
        
        public void TreeRepresentation(HyperGraphNode root) //method inherited from the interface that prints the graph as a tree
        {
            if (i == 0)
                m_dotFile.WriteLine("digraph G{");
            i++;
            foreach (HyperGraphNode node in root.m_neighbours)
            {
                if (isVisited[node.index] == false && c < node.context)
                {
                    isVisited[root.index] = true;
                    m_dotFile.Write("\"" + root.index + "\"->\"");
                    m_dotFile.Write(node.index + "\";\n");
                    Console.WriteLine("node is " + node.index);
                    TreeRepresentation(node);
                }
            }
        }
        int i = 0;
        public void UGRepresentation(HyperGraphNode root)   //method inherited from the interface that prints the graph as an Undirected Graph
        {
            if(i==0)
                m_dotFile.WriteLine("graph G{");
            i++;
            foreach (HyperGraphNode node in root.m_neighbours)
            {

                if (isVisited[node.index] == false && c < 2)
                {
                    m_dotFile.Write("\"" + root.index + "\"--\"");
                    m_dotFile.Write(node.index + "\";\n");
                    Console.WriteLine("node is " + node.index);
                    isVisited[root.index] = true;
                    UGRepresentation(node);
                }
                else if (isVisited[node.index] == true)
                {
                    m_dotFile.Write("\"" + root.index + "\"--\"");
                    m_dotFile.Write(node.index + "\";\n");
                    Console.WriteLine("node is " + node.index);
                    ++c;
                }
            }
        }

        public void DGRepresentation(HyperGraphNode root)   //method inherited from the interface that prints the graph as a Directed Graph
        { 
            if (i == 0)
                m_dotFile.WriteLine("digraph G{");
            i++;
            foreach (HyperGraphNode node in root.m_neighbours)
            {

                if (isVisited[node.index] == false && c < 2)
                {
                    m_dotFile.Write("\"" + root.index + "\"->\"");
                    m_dotFile.Write(node.index + "\";\n");
                    Console.WriteLine("node is " + node.index);
                    isVisited[root.index] = true;
                    DGRepresentation(node);
                }
                else if (isVisited[node.index] == true)
                {
                    m_dotFile.Write("\"" + root.index + "\"->\"");
                    m_dotFile.Write(node.index + "\";\n");
                    Console.WriteLine("node is " + node.index);
                    ++c;
                }
            }
        }
        public int CloseFile()  //.dot file closing method.
        {
            m_dotFile.WriteLine("}");
            m_dotFile.Close();

            // Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " + Path.GetFileName("Hyper_Graph.dot") + " -o " + Path.GetFileNameWithoutExtension("Hyper_Graph") + ".gif";
            //Enter the executable to run, including the complete path
            start.FileName = "dot";
            //Do you want to show a console window
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                // Retrieve the app's exit process
                exitCode = proc.ExitCode;
            }

            return 0;
        }

    }





    class Program {
        static void Main(string[] args) {
                Console.WriteLine("Automatically Generated Graphs of different types");
                Console.WriteLine("/////////////////////////////////////////////////////////");
                Console.WriteLine("Automatic Tree Generation and Traversing");
                //Tree Generation
                TreeNode tree_root = new TreeNode();    //Creating the tree nodes
                tree_root.index = 0;
                TreeNode child1 = new TreeNode();
                child1.index = 1;
                TreeNode child2 = new TreeNode();
                child2.index = 2;
                TreeNode child11 = new TreeNode();
                child11.index = 11;
                TreeNode child21 = new TreeNode();
                child21.index = 21;
                tree_root.addChild(child1);     //Connecting the tree nodes
                tree_root.addChild(child2);
                child1.addChild(child11);
                child2.addChild(child21);
                Console.WriteLine("Root has " + tree_root.getNumberOfChildren() + " children"); //some debug values to check if all tree node methods work
                Console.WriteLine("Child1 has " + child1.getNumberOfChildren() + " children");
                Console.WriteLine("Child2 has " + child2.getNumberOfChildren() + " children");
                Console.WriteLine("Child11 has " + child11.getNumberOfChildren() + " children");
                Tree tr = new Tree(tree_root);
                tr.TreeRepresentation(tree_root); //traversing and creating file
                tr.CloseFile(); //close file method

                //Undirected graph Generation
                Console.WriteLine("/////////////////////////////////////////////////////////");
                Console.WriteLine("Automatic Undirected Graph Generation and Traversing");
                UDiGraphNode first = new UDiGraphNode();    //Creating the undirected graph nodes
                first.index = 0;
                UDiGraphNode neighbour1 = new UDiGraphNode();
                neighbour1.index = 1;
                UDiGraphNode neighbour2 = new UDiGraphNode();
                neighbour2.index = 2;
                UDiGraphNode neighbour11 = new UDiGraphNode();
                neighbour11.index = 3;
                UDiGraphNode neighbour21 = new UDiGraphNode();
                neighbour21.index = 4;
                first.addNeighbour(neighbour1);     //Creating the undirected graph neighbour nodes
                first.addNeighbour(neighbour2);
                neighbour1.addNeighbour(neighbour11);
                neighbour2.addNeighbour(neighbour21);
                neighbour21.addNeighbour(first);
                Console.WriteLine("Root has " + first.getNumberOfNeighbours() + " children");   //some debug values to check if all undirected graph node methods work
                Console.WriteLine("Child1 has " + neighbour1.getNumberOfNeighbours() + " children");
                Console.WriteLine("Child2 has " + neighbour2.getNumberOfNeighbours() + " children");
                Console.WriteLine("Child11 has " + neighbour11.getNumberOfNeighbours() + " children");
                bool[] isVisited1 = new bool[50];
                UGraph ug = new UGraph(first, isVisited1);
                List<DiGraphNode> ret;
                ug.UGRepresentation(first);     //traversing and creating file
                ug.CloseFile();     //close file method

                //Directed graph Generation
                Console.WriteLine("/////////////////////////////////////////////////////////");
                Console.WriteLine("Automatic Directed Graph Generation and Traversing");
                DiGraphNode nod0 = new DiGraphNode();   //Creating the directed graph nodes
                nod0.index = 0;
                DiGraphNode nod1 = new DiGraphNode();
                nod1.index = 1;
                DiGraphNode nod2 = new DiGraphNode();
                nod2.index = 2;
                DiGraphNode nod3 = new DiGraphNode();
                nod3.index = 3;
                DiGraphNode nod4 = new DiGraphNode();
                nod4.index = 4;
                nod0.addDescendant(nod1);   //Connecting the directed graph nodes, parents and descendants
                nod1.addAncestor(nod0);
                nod1.addDescendant(nod3);
                nod3.addAncestor(nod1);
                nod3.addDescendant(nod1);
                nod3.addDescendant(nod2);
                nod3.addDescendant(nod4);
                nod0.addDescendant(nod2);
                nod2.addAncestor(nod0);
                nod2.addDescendant(nod4);
                nod4.addAncestor(nod2);
                nod4.addDescendant(nod0);   
                bool[] isVisited2 = new bool[50];
                DGraph dg = new DGraph(nod0, isVisited2);
                Console.WriteLine(
                "Following is Depth First Traversal "
                + "(starting from vertex 0)");
                dg.DGRepresentation(nod0);  //traversing and creating file
                dg.CloseFile();     //close file
                Console.WriteLine("/////////////////////////////////////////////////////////");
                Console.WriteLine("/////////////////////////////////////////////////////////");
                Console.WriteLine("Automatic Manual HyperGraph input");
                //Hyper graph Generation
                HyperGraphNode root = new HyperGraphNode();     //Creating the Hypergraph nodes
                root.index = 0;
                HyperGraphNode node1 = new HyperGraphNode();
                node1.index = 1;
                HyperGraphNode node2 = new HyperGraphNode();
                node2.index = 2;
                HyperGraphNode node3 = new HyperGraphNode();
                node3.index = 3;
                HyperGraphNode node4 = new HyperGraphNode();
                node4.index = 4;
                HyperGraphNode node5 = new HyperGraphNode();
                node5.index = 5;
                root.addNeighbour(node1, 1);    //Connecting the Hyper nodes with their neighbours
                node1.addNeighbour(node2, 2);
                node1.addNeighbour(node3, 2);
                node2.addNeighbour(node4, 4);
                node2.addNeighbour(node5, 4);
                node2.addNeighbour(root, 4);
                node2.addNeighbour(node2, 4);
                //I have chosen to connect the nodes above and making a cirlce, in order to show that the tree ignores closed loops and the graphs show them
                bool[] isVisited = new bool[50];
                HGraph hg = new HGraph(root, isVisited);
                Console.WriteLine("Give wanted Graph Output");
                Console.WriteLine("1. Tree Representation");
                Console.WriteLine("2. Undirected Graph");
                Console.WriteLine("3. Directed Graph");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        hg.TreeRepresentation(root);    //for Tree Graph output
                    break;

                    case 2:
                        hg.UGRepresentation(root);    //for Undirected Graph output
                    break;

                    case 3:
                        hg.DGRepresentation(root);  //for Directed Graph output
                    break;
                }
                Console.WriteLine();
                hg.CloseFile();
                Console.WriteLine("Check Bin/Debug file for outputs. There should be GIF files with the generated graphs");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
        }
    }

   
}

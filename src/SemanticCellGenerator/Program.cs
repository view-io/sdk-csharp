namespace SemanticCellGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;
    using GetSomeInput;
    using View.Sdk.Semantic;
    using View.Sdk.Serialization;

    public static class Program
    {
        static Random _Random = new Random();
        static Serializer _Serializer = new Serializer();

        public static void Main()
        {
            int topLevelCells =    Inputty.GetInteger("Number of top-level cells         :", 10, true, false);
            int maxDepth =         Inputty.GetInteger("Maximum depth (0 for no children) :", 10, true, true);
            int maxChunksPerCell = Inputty.GetInteger("Maximum chunks per cell           :", 10, true, false);

            List<SemanticCell> cells = GenerateCells(topLevelCells, maxDepth, maxChunksPerCell);

            Console.WriteLine("JSON:" + Environment.NewLine + _Serializer.SerializeJson(cells) + Environment.NewLine);
            Console.WriteLine("Minified:" + Environment.NewLine + _Serializer.SerializeJson(cells, false) + Environment.NewLine);
        }

        static List<SemanticCell> GenerateCells(int count, int maxDepth, int maxChunksPerCell, int currentDepth = 0)
        {
            var cells = new List<SemanticCell>();

            for (int i = 0; i < count; i++)
            {
                SemanticCell cell = new SemanticCell();

                // Randomly decide whether to add children (if we're not at max depth)
                // or chunks (ensuring we have at least one or the other)
                bool addChildren = maxDepth > 0 && currentDepth < maxDepth && _Random.Next(2) == 0;

                if (addChildren)
                {
                    // Generate 1-3 child cells
                    int childCount = _Random.Next(1, 4);
                    cell.Children = GenerateCells(childCount, maxDepth, maxChunksPerCell, currentDepth + 1);
                }
                else
                {
                    // Generate 1 to maxChunksPerCell chunks
                    int chunkCount = _Random.Next(1, maxChunksPerCell + 1);
                    cell.Chunks = GenerateChunks(chunkCount);
                }

                // Calculate cell's properties based on its chunks (direct and from children)
                UpdateCellProperties(cell);

                cells.Add(cell);
            }

            return cells;
        }

        static List<SemanticChunk> GenerateChunks(int count)
        {
            var chunks = new List<SemanticChunk>();
            int position = 0;

            for (int i = 0; i < count; i++)
            {
                string content = GenerateRandomParagraph();
                SemanticChunk chunk = new SemanticChunk()
                {
                    Position = position,
                    Start = 0,
                    End = content.Length - 1,
                    Length = content.Length,
                    Content = content
                };

                // Calculate hashes
                byte[] contentBytes = Encoding.UTF8.GetBytes(content);
                chunk.MD5Hash = Convert.ToHexString(MD5.Create().ComputeHash(contentBytes));
                chunk.SHA1Hash = Convert.ToHexString(SHA1.Create().ComputeHash(contentBytes));
                chunk.SHA256Hash = Convert.ToHexString(SHA256.Create().ComputeHash(contentBytes));

                chunks.Add(chunk);
                position += content.Length;
            }

            return chunks;
        }

        static string GenerateRandomParagraph()
        {
            string[] phrases = {
                // Technology & Digital
                "The quick brown fox jumps over the lazy dog.",
                "In a world full of possibilities, innovation drives progress.",
                "Technology continues to reshape our daily lives.",
                "Sustainable practices lead to a better future.",
                "Knowledge is power in the digital age.",
                "Collaboration breeds success in modern enterprises.",
                "Data-driven decisions shape business strategies.",
                "Artificial intelligence transforms industries globally.",
                "Cloud computing enables scalable solutions.",
                "Security remains paramount in digital transformation.",
                "Machine learning algorithms improve with more data.",
                "Digital transformation accelerates business growth.",
                "Edge computing brings processing closer to data sources.",
                "Blockchain technology ensures transparent transactions.",
                "The Internet of Things connects our world seamlessly.",
                "5G networks enable faster data transmission.",
                "Quantum computing promises unprecedented processing power.",
                "Cybersecurity threats evolve constantly in our connected world.",
                "Virtual reality creates immersive digital experiences.",
                "Augmented reality enhances real-world interactions.",

                // Business & Enterprise
                "Strategic planning drives organizational success.",
                "Customer experience defines brand loyalty.",
                "Innovation culture promotes creative solutions.",
                "Market dynamics influence business decisions.",
                "Supply chain optimization increases efficiency.",
                "Quality assurance ensures product reliability.",
                "Employee engagement boosts productivity levels.",
                "Risk management protects business interests.",
                "Performance metrics guide improvement efforts.",
                "Change management facilitates smooth transitions.",
                "Resource allocation optimizes business operations.",
                "Corporate governance ensures ethical practices.",
                "Market research informs product development.",
                "Brand identity shapes customer perceptions.",
                "Competitive analysis drives strategic positioning.",
                "Revenue streams diversify business models.",
                "Customer feedback shapes product evolution.",
                "Operational excellence drives market leadership.",
                "Strategic partnerships create mutual benefits.",
                "Project management ensures timely delivery.",

                // Science & Research
                "Scientific research advances human knowledge.",
                "Environmental conservation protects ecosystems.",
                "Genetic research unlocks biological mysteries.",
                "Space exploration expands human horizons.",
                "Renewable energy transforms power generation.",
                "Medical breakthroughs save countless lives.",
                "Climate change demands immediate action.",
                "Biotechnology advances medical treatments.",
                "Nanotechnology enables microscopic solutions.",
                "Research methodology ensures reliable results.",
                "Laboratory testing validates hypotheses.",
                "Scientific collaboration accelerates discovery.",
                "Evidence-based approaches guide decision making.",
                "Experimental design ensures valid results.",
                "Data analysis reveals hidden patterns.",
                "Research ethics protect human subjects.",
                "Peer review validates scientific findings.",
                "Statistical analysis quantifies relationships.",
                "Scientific innovation drives progress.",
                "Research funding enables new discoveries.",

                // Society & Culture
                "Cultural diversity enriches society.",
                "Social media connects global communities.",
                "Educational access empowers individuals.",
                "Digital literacy becomes increasingly essential.",
                "Global connectivity shrinks cultural distances.",
                "Social responsibility guides corporate actions.",
                "Community engagement strengthens societies.",
                "Digital inclusion bridges societal gaps.",
                "Cultural exchange promotes understanding.",
                "Social innovation addresses global challenges.",
                "Educational technology transforms learning.",
                "Digital communities transcend physical boundaries.",
                "Social impact measures community benefit.",
                "Cultural preservation protects heritage.",
                "Digital transformation impacts society.",
                "Social entrepreneurship creates positive change.",
                "Community development builds stronger futures.",
                "Digital ethics guide technological progress.",
                "Social networks connect diverse groups.",
                "Cultural awareness promotes harmony.",

                // Future & Innovation
                "Future technologies reshape human experience.",
                "Innovation ecosystems foster creativity.",
                "Emerging technologies create new possibilities.",
                "Future-ready solutions anticipate change.",
                "Innovative thinking drives progress.",
                "Technological advancement accelerates development.",
                "Future planning ensures sustainability.",
                "Innovation frameworks guide development.",
                "Emerging trends shape future directions.",
                "Future-proofing protects investments.",
                "Innovative solutions solve complex problems.",
                "Technological evolution continues rapidly.",
                "Future scenarios guide strategic planning.",
                "Innovation metrics measure progress.",
                "Emerging markets create new opportunities.",
                "Future generations inherit our decisions.",
                "Innovative design improves user experience.",
                "Technological literacy becomes essential.",
                "Future work embraces digital transformation.",
                "Innovation culture drives organizational success.",

                // Additional General Statements
                "Quality assurance maintains high standards.",
                "Continuous improvement drives excellence.",
                "Strategic thinking enables better decisions.",
                "Professional development enhances capabilities.",
                "Effective communication builds trust.",
                "Project success requires careful planning.",
                "Team collaboration achieves better results.",
                "Resource optimization maximizes efficiency.",
                "Performance measurement guides improvement.",
                "Risk assessment prevents potential issues.",
                "Knowledge sharing enhances team capability.",
                "Process improvement increases productivity.",
                "Customer satisfaction drives business growth.",
                "Employee development builds stronger teams.",
                "Market leadership requires constant innovation.",
                "Operational efficiency reduces costs.",
                "Strategic partnerships create value.",
                "Quality control ensures consistency.",
                "Project management delivers results.",
                "Team building strengthens organizations."
            };

            // Combine 2-4 random phrases
            int phraseCount = _Random.Next(2, 5);
            List<string> selectedPhrases = new List<string>();

            for (int i = 0; i < phraseCount; i++)
            {
                selectedPhrases.Add(phrases[_Random.Next(phrases.Length)]);
            }

            return string.Join(" ", selectedPhrases);
        }

        static void UpdateCellProperties(SemanticCell cell)
        {
            // Get all content from this cell's chunks and child cells' chunks
            List<string> allContent = GetAllContent(cell);
            string concatenatedContent = string.Join("", allContent);

            if (!String.IsNullOrEmpty(concatenatedContent))
            {
                // Calculate hashes from concatenated content
                byte[] contentBytes = Encoding.UTF8.GetBytes(concatenatedContent);
                cell.MD5Hash = Convert.ToHexString(MD5.Create().ComputeHash(contentBytes));
                cell.SHA1Hash = Convert.ToHexString(SHA1.Create().ComputeHash(contentBytes));
                cell.SHA256Hash = Convert.ToHexString(SHA256.Create().ComputeHash(contentBytes));

                // Set length
                cell.Length = concatenatedContent.Length;
            }
        }

        static List<string> GetAllContent(SemanticCell cell)
        {
            List<string> content = new List<string>();

            // Add direct chunks
            if (cell.Chunks != null)
            {
                content.AddRange(cell.Chunks.Select(c => c.Content));
            }

            // Add children's chunks
            if (cell.Children != null)
            {
                foreach (SemanticCell child in cell.Children)
                {
                    content.AddRange(GetAllContent(child));
                }
            }

            return content;
        }
    }
}
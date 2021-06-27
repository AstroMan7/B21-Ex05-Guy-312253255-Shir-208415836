

namespace Backend
{
    public class Player
    {
        public enum e_InitialTurn { First, Second }
        public enum e_Sign { Empty, O, X }
        public e_Sign m_Sign { get; set; }

        private bool m_IsComputer;

        private e_InitialTurn m_InitialTurn;

        public int m_Points { get; set; }

        public string m_Name { get; set; }

        public Player(e_Sign i_Sign, e_InitialTurn i_InitialTurn, bool i_Computer, string i_Name)
        {
            m_Name = i_Name;
            m_Sign = i_Sign;
            m_Points = 0;
            m_InitialTurn = i_InitialTurn;
            m_IsComputer = i_Computer;
        }

        public bool IsComputer()
        {
            return m_IsComputer;
        }
    }
}

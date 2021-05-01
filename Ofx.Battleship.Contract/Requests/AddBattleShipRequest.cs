namespace Ofx.Battleship.Contract.Requests
{
    public class AddBattleShipRequest
    {
        public int Size { get; set; }

        public int StartingRow { get; set; }

        public int StartingColumn { get; set; }

        public bool IsHorizontal { get; set; }
    }
}

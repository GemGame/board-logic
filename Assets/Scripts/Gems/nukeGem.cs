using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nukeGem : baseGem
{

    public override void PreDestroy()
    {

        board board = GameObject.Find("Board").GetComponent<board>();
        boardManager bm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<boardManager>();
        boardSquare[] squares = board.GetBoardStruct().StructCoreSquare;
        foreach (boardSquare square in squares)
        {
            square.Destructable = true;
            // bm.TryDestroyGem(square);
        }
    }

    public override void PostDestroy()
    {

    }
    public override void UpgradeGem()
    {

    }
    public override void HintGem()
    {

    }
}

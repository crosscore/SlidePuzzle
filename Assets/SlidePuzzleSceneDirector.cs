using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleSceneDirector : MonoBehaviour
{
    // ピースのリスト
    [SerializeField] List<GameObject> pieces;
    // クリア時に表示されるリトライボタン
    [SerializeField] GameObject buttonRetry;
    // ゲーム開始時にピースをシャッフルする回数
    [SerializeField] int shuffleCount;
    // ゲーム開始時のピースの初期位置のリスト
    List<Vector2> startPositions;
    void Start()
    {
        // 0番のピースと隣接するピースのリスト
        List<GameObject> movablePieces = new List<GameObject>();

        // 0番のピースと隣接するピースをリストに追加
        foreach (var piece in pieces) {
            if (GetEmptyPiece(piece) != null) {
                movablePieces.Add(piece);
            }
        }
        print($"movablePieces.Count: {movablePieces.Count}");

        // 隣接するピースをランダムに選択して入れ替える
        int rnd = Random.Range(0, movablePieces.Count);
        GameObject selectedPiece = movablePieces[rnd];
        SwapPiece(selectedPiece, pieces[0]);

        // ゲーム開始時はリトライボタンを非表示
        buttonRetry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // マウス入力があった場合
        if (Input.GetMouseButtonDown(0)) {
            // クリックした位置にあるピースを取得
            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // raycast でクリックした位置にあるオブジェクトを取得
            RaycastHit2D hit = Physics2D.Raycast(tapPoint, Vector2.zero);
            // 衝突判定があった場合
            if (hit) {
                // クリックしたピースを取得
                GameObject histPiece = hit.collider.gameObject;
                // クリックしたピースと0番のピースを入れ替える
                SwapPiece(histPiece, GetEmptyPiece(histPiece));
            }
        }
    }

    // 引数のピースが0番のピースと隣接していたら0番のピースを返す
    GameObject GetEmptyPiece(GameObject piece)
    {
        float dist = Vector2.Distance(piece.transform.position, pieces[0].transform.position);
        if (dist == 1) {
            return pieces[0];
        }
        return null;
    }

    // 2つのピースの位置を入れ替える
    void SwapPiece(GameObject piece1, GameObject piece2)
    {
        if (piece1 == null || piece2 == null) {
            return;
        }
        Vector2 temp = piece1.transform.position;
        piece1.transform.position = piece2.transform.position;
        piece2.transform.position = temp;
    }
}

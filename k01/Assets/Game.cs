using Sequence = System.Collections.IEnumerator;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
    int fly_x; ///飛行物の座標
    int fly_y; ///飛行物の座標
    int ki_x; ///木の座標
    int ki_y; ///木の座標
    int p_zy; ///ぺんぎんのy座標
    int p_zx; ///ぺんぎんのy座標
    int p_x; ///ぺんぎんの大きさ
    int p_y; ///ぺんぎんの大きさ
    int fire_zx;///炎の座標x
    int fire_zy;///炎の座標y
    int Y_x;
    int Y_y;

    int G; ///重力

    int jump;///ペンギンのジャンプ力
    int speed;///ペンギンの速さ
    int time;
    int road; ///距離です。
    bool Game_t;///ぺんぎんが前に進む時True。
    bool oti_t;

    int player_x;
    int player_y;

    int s_x;
    int s_y;
    int c_x;
    int c_y;
    int kan;


    public override void InitGame()
    {
        fly_x = 70000;///x座標は傾き一定的に増加します。
        fly_y = 45000;///出現時の飛行物はランダムのy座標になります。
        ki_x = 60000;///変動します。右から左に移動します。
        ki_y = 80050;///固定値です。
        p_zx = 10000; ///固定値です。
        p_zy = 10000;///ぺんぎんが飛んだり落ちたり変動します。
        p_x = 70000;///固定値です。
        p_y = 70000;///固定値です。
        fire_zx = 0;///固定値です。座標です。
        fire_zy = 110000;///固定値です。座標です。

        speed = 0;///ペンギンの速さ
        jump = 200005;///固定値です。
        time = 0;
        road = 0;
        G = 9;///固定値です。
        Game_t = true;
        oti_t = false;
        Y_x=10000;
        Y_y=10000;

        s_x=0;
        s_y=0;
        c_x=10000;
        c_y=10000;

        kan=0;

    }

    public override void UpdateGame()
    {
      if (s_x==0 && gc.GetPointerFrameCount(0) == 2){
        s_x=10000;
        s_y=10000;
        c_x=0;
        c_y=0;
        kan=1;
      }
      if (kan>=1){
        kan+=1;
      }
      if (c_x==0 && kan>3 && gc.GetPointerFrameCount(0) == 2){
        kan=0;
        resetGame();
      }

      if (Game_t == false && oti_t == false && gc.GetPointerFrameCount(0) == 2){
        player_y = gc.GetPointerY(0);
        player_x = gc.GetPointerX(0);
        if (gc.CheckHitRect(Y_x-10, Y_y, 350, 300, player_x, player_y, 10,10)){
          resetGame(); ///ゲームが終了しているときは、リセットして、Game_t=trueに。
        }
      }

      if (Game_t){
          time = time + 1; ///ゲームが実行中はtimeがプラスされます。
          p_zy = p_zy + G;///ぺんぎんは重力によって落ちます。
          ki_x = ki_x - speed; ///常に木はどんどん左に流れていく。
          fly_x = fly_x - speed; ///飛行物は1.5倍の速さで飛んできます。
      }
      road = speed * time; ///時間が進むたびに飛距離が伸びます。

      if (gc.GetPointerFrameCount(0) > 0 && Game_t ==true && p_zy>0){
          p_zy = p_zy - jump; ///タップするとジャンプ分ペンギンの座標があがります。
      }

      if (ki_x<-100){
        ki_x = 800;///木が左画面から見切れたら。右画面外に移動する。
      }
      if (fly_x<-130){
        fly_x = 800;///飛行物が左画面から見切れたら。右画面外に移動する。
        fly_y = UnityEngine.Random.Range(0, 1200); ///再出現時の飛行物はランダムのy座標になります。
      }

      if (gc.CheckHitRect(fire_zx, fire_zy, 8000, 2000, p_zx, p_zy, p_x,p_y)){
          Game_t = false; ///火に当たるとGame_tは止まります。
          oti_t = false;
          p_zx = 10000;///鳥を見えない位置に飛ばします。
          p_zy = 10000;///上に同じ。
          Y_x=100;
          Y_y=500;
          ///焼き鳥表記したいぜ。後々実装。
      }
      if (gc.CheckHitRect(fly_x, fly_y, 80, 80, p_zx, p_zy, p_x,p_y)){
          Game_t=false; ///障害物に当たるとGame_tは止まり、火に落ちていく。
          oti_t=true;
      }
      if (oti_t == true){
        p_zy = p_zy + 10; ///鳥を下に移動していく。
      }
    }

    public void resetGame(){
      fly_x = 700;///x座標は傾き一定的に増加します。
      fly_y = 450;///出現時の飛行物はランダムのy座標になります。
      ki_x = 600;///変動します。右から左に移動します。
      ki_y = 850;///固定値です。
      p_zx = 100; ///固定値です。
      p_zy = 100;///ぺんぎんが飛んだり落ちたり変動します。
      p_x = 70;///固定値です。
      p_y = 70;///固定値です。
      fire_zx = 0;///固定値です。座標です。
      fire_zy = 1100;///固定値です。座標です。

      speed = 20;///ペンギンの速さ
      jump = 25;///固定値です。
      time = 0;
      road = 0;
      G = 5;///固定値です。
      Game_t = true;
      oti_t = false;
      Y_x=10000;
      Y_y=10000;
      s_x=10000;
      s_y=10000;
      c_x=10000;
      c_y=10000;
    }
    public override void DrawGame(){
      gc.ClearScreen();
      gc.DrawImage(0, 0,0);
      gc.DrawImage(4, ki_x,ki_y);
      gc.DrawImage(8, p_zx, p_zy);
      gc.DrawImage(5, fly_x, fly_y);
      gc.DrawImage(6, fire_zx, fire_zy);
      gc.DrawImage(7, Y_x, Y_y);
      gc.DrawImage(9, s_x, s_y+100);
      gc.DrawImage(10, c_x-100, c_y+50);
      gc.SetColor(0, 0, 0);
      if(Y_x!=100 && s_x!=0){
        gc.SetFontSize(60);
        gc.DrawString("飛んだ距離:" + road, 60, 0);
      }

      if(Y_x==100){
        if(road<10000){
          gc.SetFontSize(50);
          gc.DrawString("こりゃ、ただのやきとりだよ", 50, 400);
        }
        if(road>=10000 && road<20000){
          gc.SetFontSize(50);
          gc.DrawString("こりゃ、すげぇやきとりだよ", 50, 400);
        }
        if(road>=20000){
          gc.SetFontSize(50);
          gc.DrawString("これは、伝説のやきとりかも?", 50, 400);
        }
        if(road>=50000){
          gc.SetFontSize(50);
          gc.DrawString("これは、伝説に残る焼き鳥だよ", 50, 400);
        }
            gc.SetFontSize(50);
            gc.DrawString("結果 : "+road+"だけ飛べたけど", 10, 200);
            gc.SetColor(255, 0, 0);
            gc.SetFontSize(40);
            gc.DrawString("もう一度挑戦するなら肉をタップ", Y_x-50, Y_y+300);
      }
    }
  }

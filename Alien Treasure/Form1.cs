using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alien_Adventure
{
    public partial class Form1 : Form
    {
        private Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(this);

        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DoubleBuffered = true;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            game.paint(e.Graphics);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(game.getState()))
            {
                if (e.KeyCode == Keys.Y)
                {
                    game = null;
                    game = new Game(this);
                }
                else if (e.KeyCode == Keys.N)
                {
                    this.Close();
                }
            }
            else
            {
                game.keyDown(e);
            }
        }

    }

    public class Alien
    {
        #region fields
        private bool state = true;
        private bool dead = false;
        private bool attacking = false;
        private bool returning = false;
        private int x;
        private int y;
        private int treasure = 0;
        private int rank;
        private Image image;
        #endregion

        public void setImage(Image image)
        {
            this.image = image;
        }

        public void setState(bool state)
        {
            this.state = state;
        }

        public void kill()
        {
            this.dead = true;
        }

        public void attack()
        {
            this.attacking = true;
        }

        public void stopAttack()
        {
            this.attacking = false;
        }

        public void startReturn()
        {
            this.returning = true;
        }

        public void finishReturn()
        {
            this.returning = false;
        }

        public void setTreasure(int treasure)
        {
            this.treasure = treasure;
        }

        public void setRank(int rank)
        {
            this.rank = rank;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public bool getState()
        {
            return this.state;
        }

        public bool isDead()
        {
            return this.dead;
        }

        public bool isReturning()
        {
            return this.returning;
        }

        public bool inAttack()
        {
            return this.attacking;
        }

        public int getTreasure()
        {
            return this.treasure;
        }

        public int getRank()
        {
            return this.rank;
        }

        public Image getImage()
        {
            return this.image;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }
    }

    public class Player
    {
        private Image image;
        private bool state = true;
        private bool gotTreasure = false;
        private int score = 0;
        private int fuel = 300;
        private int ammo = 100;
        private int lifes = 3;
        private int x = 140;
        private int y = 360;

        public void setImage(Image image)
        {
            this.image = image;
        }

        public void setState(bool state)
        {
            this.state = state;
        }

        public void setTreasureState(bool state)
        {
            this.gotTreasure = state;
        }

        public void setX(int x)
        {
            this.x += x;
        }

        public void setY(int y)
        {
            this.y += y;
        }

        public void setScore(int score)
        {
            this.score += score;
        }

        public void setFuel(int fuel)
        {
            this.fuel += fuel;
        }

        public void setAmmo(int ammo)
        {
            this.ammo += ammo;
        }

        public void setLifes(int life)
        {
            this.lifes += life;
        }

        public bool getState()
        {
            return this.state;
        }

        public bool getTreasureState()
        {
            return this.gotTreasure;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public int getScore()
        {
            return this.score;
        }

        public int getFuel()
        {
            return this.fuel;
        }

        public int getAmmo()
        {
            return this.ammo;
        }

        public int getLifes()
        {
            return this.lifes;
        }

        public Image getImage()
        {
            return this.image;
        }
        
    }

    public class Missile
    {
        private Image image;
        private bool state = true;
        private int x;
        private int y;
        private int type;

        public void setImage(Image image)
        {
            this.image = image;
        }

        public void setState(bool state)
        {
            this.state = state;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public void setX(int x)
        {
            this.x += x;
        }

        public void setY(int y)
        {
            this.y += y;
        }

        public bool getState()
        {
            return this.state;
        }

        public int getType()
        {
            return this.type;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public Image getImage()
        {
            return this.image;
        }

    }

    public class Treasure
    {
        private Image image;
        private bool active = true;
        private int x;
        private int y;
        private int type;

        public void setImage(Image image)
        {
            this.image = image;
        }

        public void setState(bool active)
        {
            this.active = active;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public void setX(int x)
        {
            this.x += x;
        }

        public void setY(int y)
        {
            this.y += y;
        }

        public bool getState()
        {
            return this.active;
        }

        public int getType()
        {
            return this.type;
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public Image getImage()
        {
            return this.image;
        }
    }

    public class Game
    {
        #region declarations
        private const int ALL_ALIENS = 30;
        private const int DOT_SIZE = 20;
        private const int DELAY = 700;
        private int steps = 2;
        private int attackSteps = 1;
        private int alienShots = 1;
        private int missileCount = 0;
        private int treasureCount = 0;
        private bool right = true;
        private bool inGame = true;
        private Player player = new Player();
        private Alien[] aliens = new Alien[ALL_ALIENS];
        private Missile[] missiles = new Missile[1000];
        private Treasure[] treasures = new Treasure[1000];
        private int[][] rightAttack = new int[7][];
        private int[][] leftAttack = new int[7][];
        private int[] intArrX = new int[ALL_ALIENS];
        private int[] intArrY = new int[ALL_ALIENS];
        private Timer timer;
        private Timer missileTimer;
        private Timer treasureTimer;
        private Timer attackTimer;
        private Timer returnTimer;
        private Form parent;
        enum dir {RIGHT, LEFT };
        dir attackDir;
        #endregion

        public Game(Form form)
        {
            //set parent
            this.parent = form;
            //initialize game
            initGame();
        }

        private void initGame()
        {
            //create the aliens
            for (int i = 0; i < ALL_ALIENS; i++)
            {
                aliens[i] = new Alien();
            }

            //set player image
            player.setImage(Properties.Resources.player);

            //set alien commander attributes
            aliens[0].setRank(1);
            aliens[0].setTreasure(1);
            aliens[0].setImage(Properties.Resources.alienRed);
            
            Random rnd = new Random();

            //set alien generals attributes
            for (int i = 1; i < 4; i++)
            {
                aliens[i].setRank(2);
                int gift = rnd.Next(0, 2);
                if(gift == 1)
                    aliens[i].setTreasure(rnd.Next(0, 6));
                aliens[i].setImage(Properties.Resources.alienYellow);
            }
            //set alien captains attributes
            for (int i = 4; i < 9; i++)
            {
                aliens[i].setRank(3);
                int gift = rnd.Next(0, 2);
                if (gift == 1)
                    aliens[i].setTreasure(rnd.Next(0, 6));
                aliens[i].setImage(Properties.Resources.alienBlue);
            }
            //set alien privates attributes
            for (int i = 9; i < ALL_ALIENS; i++)
            {
                aliens[i].setRank(4);
                int gift = rnd.Next(0, 2);
                if (gift == 1)
                    aliens[i].setTreasure(rnd.Next(0, 6));
                aliens[i].setImage(Properties.Resources.alienGreen);
              
            }

            intArrX[9] = intArrX[16] = intArrX[23] = 40;
            intArrX[4] = intArrX[10] = intArrX[17] = intArrX[24] = 70;
            intArrX[1] = intArrX[5] = intArrX[11] = intArrX[18] = intArrX[25] = 100;
            intArrX[0] = intArrX[2] = intArrX[6] = intArrX[12] = intArrX[19] = intArrX[26] = 130;
            intArrX[3] = intArrX[7] = intArrX[13] = intArrX[20] = intArrX[27] = 160;
            intArrX[8] = intArrX[14] = intArrX[21] = intArrX[28] = 190;
            intArrX[15] = intArrX[22] = intArrX[29] = 220;

            intArrY[0] = 60;
            intArrY[1] = intArrY[2] = intArrY[3] = 85;
            intArrY[4] = intArrY[5] = intArrY[6] = intArrY[7] = intArrY[8] = 110;
            intArrY[9] = intArrY[10] = intArrY[11] = intArrY[12] = intArrY[13] = intArrY[14] = intArrY[15] = 135;
            intArrY[16] = intArrY[17] = intArrY[18] = intArrY[19] = intArrY[20] = intArrY[21] = intArrY[22] = 160;
            intArrY[23] = intArrY[24] = intArrY[25] = intArrY[26] = intArrY[27] = intArrY[28] = intArrY[29] = 185;

            for (int i = 0; i < ALL_ALIENS; i++)
            {
                aliens[i].setX(intArrX[i]);
                aliens[i].setY(intArrY[i]);
            }
            
            rightAttack[0] = new int[]{15,22,29};
            rightAttack[1] = new int[]{8,14,21,28};
            rightAttack[2] = new int[]{3,7,13,20,27};
            rightAttack[3] = new int[]{0,2,6,12,19,26};
            rightAttack[4] = new int[]{1,5,11,18,25};
            rightAttack[5] = new int[]{4,10,17,24};
            rightAttack[6] = new int[]{9,16,23};

            leftAttack[0] = new int[]{9,16,23};
            leftAttack[1] = new int[]{4,10,17,24};
            leftAttack[2] = new int[]{1,5,11,18,25};
            leftAttack[3] = new int[]{0,2,6,12,19,26};
            leftAttack[4] = new int[]{3,7,13,20,27};
            leftAttack[5] = new int[]{8,14,21,28};
            leftAttack[6] = new int[]{15,22,29};

            missileTimer = new Timer();
            missileTimer.Interval = 5;
            missileTimer.Tick += missileTimer_Tick;

            treasureTimer = new Timer();
            treasureTimer.Interval = 10;
            treasureTimer.Tick += treasureTimer_Tick;

            attackTimer = new Timer();
            attackTimer.Interval = 200;
            attackTimer.Tick += attackTimer_Tick;

            returnTimer = new Timer();
            returnTimer.Interval = 150;
            returnTimer.Tick += returnTimer_Tick;

            timer = new Timer();
            timer.Interval = DELAY;
            timer.Tick += updateGame;
            timer.Start();

        }

        public bool getState()
        {
            return this.inGame;
        }

        private void move()
        {
            if (right)
            {
                //increment the X coordinate of all aliens by 20
                for (int i = 0; i < ALL_ALIENS; i++)
                {
                    intArrX[i] += 20;
                    if(!aliens[i].inAttack())
                        aliens[i].setX(aliens[i].getX()+20);
                }
                //increment steps count and change direction if steps == 5
                steps++;
                if (steps == 5)
                {
                    steps = 0;
                    right = false;
                    if (attackDir != dir.LEFT)
                        attack();

                }
            }
            else
            {
                //decrement the X coordinate of all aliens by 20
                for (int i = 0; i < ALL_ALIENS; i++)
                {
                    intArrX[i] += -20;
                    if (!aliens[i].inAttack())
                        aliens[i].setX(aliens[i].getX()-20);
                }
                //increment steps count and change direction if steps == 5
                steps++;
                if (steps == 5)
                {
                    steps = 0;
                    right = true;
                    if (attackDir != dir.RIGHT)
                        attack();
                    
                }
            }
        }

        private void attack()
       {
           bool attacking = false;

           for (int i = 0; i < ALL_ALIENS; i++)
           {
               if (attacking = aliens[i].inAttack())
                   break;
           }

           if (!attacking)
           {
               if (right)
               {
                   attackDir = dir.RIGHT;
                   for (int i = 0; i < 7; i++)
                   {
                       int j;
                       for (j = 0; j < rightAttack[i].Length; j++)
                       {
                           if (aliens[rightAttack[i][j]].getState())
                           {
                               aliens[rightAttack[i][j]].attack();
                               break;
                           }
                       }

                       if (j < rightAttack[i].Length)
                           break;
                   }
               }
               else
               {
                   attackDir = dir.LEFT;
                   for (int i = 0; i < 7; i++)
                   {
                       int j;
                       for (j = 0; j < leftAttack[i].Length; j++)
                       {
                           if (aliens[leftAttack[i][j]].getState())
                           {
                               aliens[leftAttack[i][j]].attack();
                               break;
                           }
                       }

                       if (j < leftAttack[i].Length)
                           break;
                   }
               }

               if (attackTimer.Enabled == false)
               {
                   attackTimer.Start();
               }
           } 
       }

        private void checkCollision()
        {
            for (int i = 0; i < missileCount; i++)
            {
                if (missiles[i].getState())
                {
                    if (missiles[i].getType() == 0)
                    {
                        for (int j = 0; j < ALL_ALIENS; j++)
                        {
                            if (aliens[j].getState())
                            {
                                if (missiles[i].getX() >= aliens[j].getX() && missiles[i].getX() < aliens[j].getX() + DOT_SIZE &&
                                    missiles[i].getY() >= aliens[j].getY() && missiles[i].getY() < aliens[j].getY() + DOT_SIZE ||
                                    missiles[i].getX() + 5 > aliens[j].getX() && missiles[i].getX() + 5 <= aliens[j].getX() + DOT_SIZE &&
                                    missiles[i].getY() + 5 > aliens[j].getY() && missiles[i].getY() + 5 <= aliens[j].getY() + DOT_SIZE)
                                {
                                    if (aliens[j].inAttack())
                                    {
                                        aliens[j].stopAttack();
                                        attackTimer.Interval = 200;
                                        attackTimer.Stop();
                                        attackSteps = 1;
                                        alienShots = 1;
                                    }
                                    
                                    aliens[j].setImage(Properties.Resources.alienDead);
                                    SoundPlayer media = new SoundPlayer(Properties.Resources.alienExplode);
                                    media.Load();
                                    media.Play();
                                    player.setScore(30);
                                    timer.Interval = 700;
                                    aliens[j].kill();
                                    missiles[i].setState(false);

                                    if (aliens[j].getTreasure() != 0)
                                    {
                                        treasures[treasureCount] = new Treasure();
                                        treasures[treasureCount].setX(aliens[j].getX());
                                        treasures[treasureCount].setY(aliens[j].getY());
                                        treasures[treasureCount].setType(aliens[j].getTreasure());

                                        switch (aliens[j].getTreasure())
                                        {
                                            case 1:
                                                treasures[treasureCount].setImage(Properties.Resources.life);
                                                break;
                                            case 2:
                                                treasures[treasureCount].setImage(Properties.Resources.treasure);
                                                break;
                                            case 3:
                                                treasures[treasureCount].setImage(Properties.Resources.fuel);
                                                break;
                                            case 4:
                                                treasures[treasureCount].setImage(Properties.Resources.ammo);
                                                break;
                                            case 5:
                                                treasures[treasureCount].setImage(Properties.Resources.toxic);
                                                break;
                                        }

                                        treasureCount++;
                                        if (treasureTimer.Enabled == false)
                                        {
                                            treasureTimer.Start();
                                        }
                                    }

                                    if (aliens[j].inAttack())
                                    {
                                        aliens[j].stopAttack();
                                        attackTimer.Stop();
                                    }
                                }

                                if (missiles[i].getY() < 0)
                                    missiles[i].setState(false);
                            }
                        }
                    }
                    else
                    {
                        if (missiles[i].getX() >= player.getX() && missiles[i].getX() < player.getX() + DOT_SIZE &&
                            missiles[i].getY() >= player.getY() && missiles[i].getY() < player.getY() + DOT_SIZE ||
                            missiles[i].getX() + 5 > player.getX() && missiles[i].getX() + 5 <= player.getX() + DOT_SIZE &&
                            missiles[i].getY() + 5 > player.getY() && missiles[i].getY() + 5 <= player.getY() + DOT_SIZE)
                        {
                            SoundPlayer media = new SoundPlayer(Properties.Resources.alienExplode);
                            media.Load();
                            media.Play();
                            player.setImage(Properties.Resources.playerDead);
                            player.setLifes(-1);
                            timer.Interval = 700;
                            missiles[i].setState(false);
                        }

                        if (missiles[i].getY() > 400)
                            missiles[i].setState(false);

                        break;
                    } 
                }  
            }
        }

        private void checkGameState()
        {
            if (player.getLifes() <= 0)
            {
                inGame = false;
            }

            if (player.getFuel() <= 0)
            {
                inGame = false;
            }

            if (player.getAmmo() <= 0)
            {
                inGame = false;
            }
        }

        private void checkTreasureState()
        {
            for (int i = 0; i < treasureCount; i++)
            {
                if (treasures[i].getState())
                {
                    if (treasures[i].getX() >= player.getX() && treasures[i].getX() < player.getX() + DOT_SIZE &&
                    treasures[i].getY() >= player.getY() && treasures[i].getY() < player.getY() + DOT_SIZE ||
                    treasures[i].getX() + 5 > player.getX() && treasures[i].getX() + 5 <= player.getX() + DOT_SIZE &&
                    treasures[i].getY() + 5 > player.getY() && treasures[i].getY() + 5 <= player.getY() + DOT_SIZE)
                    {
                        switch (treasures[i].getType())
                        {
                            case 1:
                                player.setImage(Properties.Resources.gotTreasure);
                                player.setLifes(1);
                                break;
                            case 2:
                                player.setImage(Properties.Resources.gotTreasure);
                                player.setScore(60);
                                break;
                            case 3:
                                player.setImage(Properties.Resources.gotTreasure);
                                player.setFuel(50);
                                break;
                            case 4:
                                player.setImage(Properties.Resources.gotTreasure);
                                player.setAmmo(50);
                                break;
                            case 5:
                                player.setImage(Properties.Resources.playerDead);
                                player.setLifes(-1);
                                break;
                        }

                        treasures[i].setState(false);
                        player.setTreasureState(true);
                        SoundPlayer media = new SoundPlayer(Properties.Resources.bonus);
                        media.Load();
                        media.Play();
                        timer.Interval = 700;
                    }

                    if (treasures[i].getY() > 400)
                        treasures[i].setState(false);
                }
            }
        }

        private void checkAttackState()
        {
            for (int i = 0; i < ALL_ALIENS; i++)
            {
                if (aliens[i].getState() && aliens[i].inAttack())
                {
                    if (aliens[i].getX() >= player.getX() && aliens[i].getX() < player.getX() + DOT_SIZE &&
                        aliens[i].getY() >= player.getY() && aliens[i].getY() < player.getY() + DOT_SIZE ||
                        aliens[i].getX() + DOT_SIZE > player.getX() && aliens[i].getX() + DOT_SIZE <= player.getX() + DOT_SIZE && 
                        aliens[i].getY() + DOT_SIZE > player.getY() && aliens[i].getY() + DOT_SIZE <= player.getY() + DOT_SIZE)
                    {
                        aliens[i].setImage(Properties.Resources.alienDead);
                        SoundPlayer media = new SoundPlayer(Properties.Resources.alienExplode);
                        media.Load();
                        media.Play();
                        player.setScore(30);
                        player.setImage(Properties.Resources.playerDead);
                        player.setLifes(-1);
                        timer.Interval = 700;
                        aliens[i].stopAttack();
                        aliens[i].kill();
                        attackTimer.Interval = 200;
                        attackTimer.Stop();
                        attackSteps = 1;
                        alienShots = 1;
                    }

                    if (aliens[i].getX() > 300 || aliens[i].getX() + DOT_SIZE < 0 || aliens[i].getY() > 400)
                    {
                        aliens[i].stopAttack();
                        attackTimer.Interval = 200;
                        attackTimer.Stop();
                        attackSteps = 1;
                        alienShots = 1;
                        returnToFormation(aliens[i], i);
                    }
                    break;
                }
            }

            if (player.getLifes() > 0)
                player.setImage(Properties.Resources.player);

            parent.Invalidate();
        }

        private void returnToFormation(Alien alien, int pos)
        {
            alien.setY(0);
            alien.startReturn();
            alien.setX(intArrX[pos]);
            
            if (!returnTimer.Enabled)
                returnTimer.Start();
        }

        private void updateGame(object sender, EventArgs e)
        {
            move();
            parent.Invalidate();
            checkGameState();
        }

        private void missileTimer_Tick(object sender, EventArgs e)
        {
            if (missileCount > 0)
            {
                for (int i = 0; i < missileCount; i++)
                {
                    if (missiles[i].getType() == 0)
                    {
                        if (missiles[i].getState())
                            missiles[i].setY(-5);
                    }
                    else
                    {
                        if (missiles[i].getState())
                            missiles[i].setY(3);
                    }
                }

                parent.Invalidate();
                checkCollision();

            }
            else
            {
                missileTimer.Stop();
            }
        }

        private void treasureTimer_Tick(object sender, EventArgs e)
        {
            if (treasureCount > 0)
            {
                for (int i = 0; i < treasureCount; i++)
                {
                    if (treasures[i].getState())
                        treasures[i].setY(3);
                }

                parent.Invalidate();
                checkTreasureState();

            }
            else
            {
                treasureTimer.Stop();
            }
        }

        private void attackTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ALL_ALIENS; i++)
            {
                //if alien is alive and in attack
                if (aliens[i].getState() && aliens[i].inAttack())
                {
                    if (attackSteps <= 6)
                    {
                        if (attackSteps == 1)
                        {
                            if (attackDir == dir.RIGHT)
                            {
                                //go 20px up and to the right
                                aliens[i].setX(aliens[i].getX()+20);
                                aliens[i].setY(aliens[i].getY()-20);
                            }
                            else
                            {
                                //go 20px up and to the left
                                aliens[i].setX(aliens[i].getX()-20);
                                aliens[i].setY(aliens[i].getY()-20);
                            }
                        }
                        else if (attackSteps == 2)
                        {
                            if (attackDir == dir.RIGHT)
                                //continue 20px right
                                aliens[i].setX(aliens[i].getX()+20); 
                            else
                                //continue 20px left
                                aliens[i].setX(aliens[i].getX()-20); 
                        }
                        else if (attackSteps == 3 || attackSteps == 4)
                        {
                            if (attackDir == dir.RIGHT)
                            {
                                //go 20px down and to the right
                                aliens[i].setX(aliens[i].getX()+20);
                                aliens[i].setY(aliens[i].getY()+20);
                            }
                            else
                            {
                                //go 20px down and to the left
                                aliens[i].setX(aliens[i].getX()-20);
                                aliens[i].setY(aliens[i].getY()+20);
                            }
                        }
                        else if (attackSteps == 5 || attackSteps == 6)
                        {
                            //continue 20px down
                            aliens[i].setY(aliens[i].getY()+20);
                        }
                    }
                    else
                    {
                        //slow down the attack
                        attackTimer.Stop();
                        attackTimer.Interval = 300;
                        attackTimer.Start();

                        if (attackDir == dir.RIGHT)
                        {
                            //go 20px down and to the left
                            aliens[i].setX(aliens[i].getX()-20);
                            aliens[i].setY(aliens[i].getY()+20);
                        }
                        else
                        {
                            //go 20px down and to the right
                            aliens[i].setX(aliens[i].getX()+20);
                            aliens[i].setY(aliens[i].getY()+20);
                        }

                        //if alien is close enough and still has shots
                        if (alienShots <= 3 && attackSteps >= 9)
                        {
                            missiles[missileCount] = new Missile();
                            missiles[missileCount].setX(aliens[i].getX() + 7);
                            missiles[missileCount].setY(aliens[i].getY() + 5);
                            missiles[missileCount].setType(1);
                            missiles[missileCount].setImage(Properties.Resources.missile);
                            missileCount++;
                            alienShots++;

                            if (missileTimer.Enabled == false)
                            {
                                missileTimer.Start();
                            }
                        }
                    }
                    break;
                }
            }

            attackSteps++;
            parent.Invalidate();
            checkAttackState();
        }

        private void returnTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ALL_ALIENS; i++)
            {
                if (aliens[i].getState() && aliens[i].isReturning())
                {
                    if (aliens[i].getY() < intArrY[i])
                    {
                        aliens[i].setY(aliens[i].getY() + ((aliens[i].getY() < 60)? 20 : 25));
                    }
                    else
                    {
                        aliens[i].finishReturn();
                        returnTimer.Stop();
                    }
                }
            }
        }

        public void paint(Graphics g)
        {
            if (inGame)
            {

                for (int i = 0; i < ALL_ALIENS; i++)
                {
                    //draw the aliens
                    if (aliens[i].getState())
                    {
                        g.DrawImage(aliens[i].getImage(), aliens[i].getX(), aliens[i].getY());
                        if (aliens[i].isDead())
                            aliens[i].setState(false);
                        
                    }
                }

                //draw the player
                g.DrawImage(player.getImage(), player.getX(), player.getY());
                if (player.getTreasureState())
                {
                    player.setTreasureState(false);
                    //reset player image
                    player.setImage(Properties.Resources.player);

                }

                if (missileCount > 0)
                {
                    for (int i = 0; i < missileCount; i++)
                    {
                        //draw the missile
                        if(missiles[i].getState())
                            g.DrawImage(missiles[i].getImage(), missiles[i].getX(), missiles[i].getY());
                    }
                }

                if (treasureCount > 0)
                {
                    for (int i = 0; i < treasureCount; i++)
                    {
                        //draw the missile
                        if (treasures[i].getState())
                            g.DrawImage(treasures[i].getImage(), treasures[i].getX(), treasures[i].getY());
                    }
                }

                //draw the score on the screen
                SolidBrush brush = new SolidBrush(Color.White);
                Font font = new Font(new FontFamily("Arial"), 10, FontStyle.Bold);
                g.DrawString("Score: " + player.getScore(), font, brush, new PointF(5, 5));
                g.DrawString("Fuel: " + player.getFuel(), font, brush, new PointF(85, 5));
                g.DrawString("Ammo: " + player.getAmmo(), font, brush, new PointF(155, 5));
                g.DrawString("Lifes: " + player.getLifes(), font, brush, new PointF(235, 5));
                //draw line to seperate score section with game area
                Pen pen = new Pen(brush, 2);
                g.DrawLine(pen, new PointF(0, 28), new PointF(320, 28));

            }
            else
            {
                gameOver(g);
            }
        }

        private void gameOver(Graphics g)
        {
            /*//draw the string Game Over! on the screen
            SolidBrush brush = new SolidBrush(Color.White);
            Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Bold);
            g.DrawString("Game Over!", font, brush, new PointF((WIDTH / 2) - 40, (HEIGHT / 2) - 5));
            g.DrawString("Play again? Enter Y/N", font, brush, new PointF((WIDTH / 2) - 80, (HEIGHT / 2) + 10));*/

        }

        public void keyDown(KeyEventArgs e)
        {
            //change direction depending on pressed key
            Keys key = e.KeyCode;

            if ((key == Keys.NumPad4) && player.getX() > 0 && player.getFuel() > 0)
            {
                player.setX(-5);
                player.setFuel(-1);
            }

            if ((key == Keys.NumPad6) && player.getX() < 280 && player.getFuel() > 0)
            {
                player.setX(5);
                player.setFuel(-1);
            }

            if ((key == Keys.Space) && player.getAmmo() > 0)
            {
                missiles[missileCount] = new Missile();
                missiles[missileCount].setX(player.getX() + 7);
                missiles[missileCount].setY(player.getY() - 5);
                missiles[missileCount].setType(0);
                missiles[missileCount].setImage(Properties.Resources.bullet);
                missileCount++;
                player.setAmmo(-1);
                SoundPlayer media = new SoundPlayer(Properties.Resources.laser);
                media.Load();
                media.Play();

                if (missileTimer.Enabled == false)
                {
                    missileTimer.Start();
                }
            }

            parent.Invalidate();
        }
    }
}

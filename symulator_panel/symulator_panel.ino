#include <LiquidCrystal_PCF8574.h>      //I2C LCD Library

LiquidCrystal_PCF8574 lcd(0x3f);        //Address of LCD

int VRy = A0;     //Joystick Y axis input pin
int VRx = A1;     //Joystick X axis input pin
int TPS = A3;     //Throttle Position Sensor input pin
int RPS = A2;     //Rudder Position Sensor input pin
int pause = 2;    //Pause button pin
int unpause = 3;    //Unpause button pin
                    
int VRy_val = 0;    //Joystick Y axis value
int VRx_val = 0;    //Joystick X axis value
int TPS_val = 0;    //Throttle Position Sensor value
int RPS_val = 0;    //Rudder Position Sensor value

// UNITY ////////////////////////////////////////////
float VSS_val = 0;    //Vehicle Speed Sensor value
float RUD_val = 0;    //Rudder value
float DEP_val = 0;    //Depth value
float COG_val = 0;    //Course Over Ground value
float ROT_val = 0;    //Rate of turn value
// UNITY ////////////////////////////////////////////

void setup() {
  Serial.begin(9200);       //open serial port
  lcd.begin(16, 2);         //LCD init
  lcd.setBacklight(255);    //Backlight 100%
  pinMode(2, INPUT_PULLUP); //Pause button config
  pinMode(3, INPUT_PULLUP); //Reset button config
  lcd.clear();
  lcd.setCursor(2, 0);
  lcd.print("Calibration");
  lcd.setCursor(1, 1);
  lcd.print("Please wait...");
  delay(3000);              //Delay to ensure, that everything has been done
}

void loop() {
  int TimeControll = 0;     //Split program with time markers


  if(Serial.read() != -1){  //Check if there is some data on serial port
    
    while(TimeControll == 0 || TimeControll == 10 || TimeControll == 15 || TimeControll == 20)
{
                    { //SPEED DISPLAY ON LCD    
                  lcd.clear();
                  lcd.setCursor(0, 0);
                  lcd.print("Speed:");
                  lcd.setCursor(7, 0);
                  lcd.print(VSS_val,1);
                  lcd.setCursor(11, 0);
                  lcd.print(" kts");
                    }
              
                    { //DEPTH DISPLAY ON LCD
                  lcd.setCursor(0, 1);
                  lcd.print("Rudder:");
                  lcd.setCursor(8, 1);
                  lcd.print("any");
                  lcd.setCursor(12, 1);
                  lcd.print("data");
                    }
 
  TimeControll++;
}

    while(TimeControll > 0 && TimeControll < 20)
{
  VRy_val = analogRead(VRy);    //Joystick Y axis readout
  VRx_val = analogRead(VRx);    //Joystick X axis readout
  TPS_val = analogRead(TPS);    //Throttle Position Sensor readout
  RPS_val = analogRead(RPS);    //Throttle Position Sensor readout

  VRy_val = map(VRy_val, 0, 1004, -50, 50);       //map() is used to transform UART data into values you need in program
  VRx_val = map(VRx_val, 0, 1012, -50, 50);
  TPS_val = map(TPS_val, 0, 1018, -100, 100);
  RPS_val = map(RPS_val, 0, 1019, -35, 35);
  
 
      { //PRINT TO SERIAL PORT
        Serial.print(VRx_val);
        Serial.print(",");
      
        Serial.print(VRy_val);
        Serial.print(",");
      
        Serial.print(TPS_val);
        Serial.print(",");

        Serial.print(RPS_val);
        Serial.print(",");
        
        Serial.print(digitalRead(pause));
        Serial.print(",");

        Serial.print(digitalRead(unpause));
        Serial.print(",");
        
        Serial.println("");
      }

       { //READ FROM SERIAL PORT
        VSS_val = Serial.parseFloat();
     
      }
  
  delay(25); // wait 25ms
  TimeControll++;
}
  }
  else      //if there is no data on serial, then shows this on screen and wait for program to run
  {
                  lcd.clear();
                  lcd.setCursor(0, 0);
                  lcd.print("Symulator Morski");
                  lcd.setCursor(0, 1);
                  lcd.print("waiting for app");
                  delay(1000);
                  
  } 
}

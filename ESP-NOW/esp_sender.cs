#include <esp_now.h>
#include <WiFi.h>

// Reference voltage
#define ADC_VREF_mV 3300.0 
//12 bits
#define ADC_RESOLUTION 4096.0 
// Pin connected to thermometer
#define PIN_LM35 35 

//A8:03:2A:6B:6C:D8 
uint8_t broadcastAddress[] = {0xA8, 0x03, 0x2A, 0x6B, 0x6C, 0xD8};

// Structure of the message is first 32 chars and then a float
typedef struct struct_message {
  char a[32];
  float c;
  
} struct_message;

// Create the message
struct_message myData;

esp_now_peer_info_t peerInfo;

// Function to determine if delivery is a success
void OnDataSent(const uint8_t *mac_addr, esp_now_send_status_t status) {
  Serial.print("\r\nLast Packet Send Status:\t");
  Serial.println(status == ESP_NOW_SEND_SUCCESS ? "Delivery Success" : "Delivery Fail");
}
 
void setup() {
  
  Serial.begin(115200);
 
  // ESP-NOW uses some properties of wifistack
  WiFi.mode(WIFI_STA);


  // Initialize ESP-NOW
  if (esp_now_init() != ESP_OK) {
    Serial.println("Error initializing ESP-NOW");
    return;
  }

  // Register callback function. Every time ESP-NOW sends message the OnDataSent function is called
  esp_now_register_send_cb(OnDataSent);
  
  // Register peer, copy the recipent MAC address to peer address
  memcpy(peerInfo.peer_addr, broadcastAddress, 6);
  peerInfo.channel = 0;  
  peerInfo.encrypt = false;
  
  // Let's add  peer     
  if (esp_now_add_peer(&peerInfo) != ESP_OK){
    Serial.println("Failed to add peer");
    return;
  }
}
 
void loop() {

  // Let's read the pin of the thermometer
  int adcVal = analogRead(PIN_LM35); 
  // Then multiply the pinreading with the smallest relevant bit
  float milliVolt = adcVal * (ADC_VREF_mV / ADC_RESOLUTION); 
  // Millivolts can be converted to Celsius by dividing them by ten
  float tempC = milliVolt / 10;
  // Let's copy the string to message char 32 place in the message structure
  strcpy(myData.a, "Temperature:");
  // Float in the message structure will be the reading of the thermometer
  myData.c = tempC;
  
  // Stores the info if the transmission was success in the variable
  esp_err_t result = esp_now_send(broadcastAddress, (uint8_t *) &myData, sizeof(myData));
   
  if (result == ESP_OK) {
    Serial.println("Sent with success");
  }
  else {
    Serial.println("Error sending the data");
  }
  delay(2000);
}

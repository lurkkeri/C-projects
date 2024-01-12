#include <esp_now.h>
#include <WiFi.h>

// Message structure of the incoming message
typedef struct struct_message {
    char a[32];
    float c;

} struct_message;

struct_message myData;

// Function to receive the data
void OnDataRecv(const uint8_t * mac, const uint8_t *incomingData, int len) {
  memcpy(&myData, incomingData, sizeof(myData));
  Serial.print("Bytes received: ");
  Serial.println(len);
  // String "Temperature"
  Serial.println(myData.a); 
  // Temperature in Celsius
  Serial.println(myData.c); 

}
 
void setup() {
  
  Serial.begin(115200);
  
  WiFi.mode(WIFI_STA);

  // Intialize ESP-NOW
  if (esp_now_init() != ESP_OK) {
    Serial.println("Error initializing ESP-NOW");
    return;
  }
  
// Register the callback function for receiving datas
  esp_now_register_recv_cb(OnDataRecv);
}
 
void loop() {

}
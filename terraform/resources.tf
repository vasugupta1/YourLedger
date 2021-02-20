#GCP Json Storage
resource "google_storage_bucket" "YourLedger-Json-bucket" {
  name          = "yourledger-storage"
  location      = "EU"
  force_destroy = false
}
#GCP function code storage:used to store assets realted to function
resource "google_storage_bucket" "YourLedger-Function-bucket" {
  name          = "yourledger-function-storage"
  location      = "EU"
  force_destroy = false
}

resource "google_storage_bucket_object" "YourLedger-Function-Code" {
  name   = basename(var.function-code-zip-location)
  bucket = google_storage_bucket.YourLedger-Function-bucket.name
  source = var.function-code-zip-location
}

#Pub&Sub
resource "google_pubsub_topic" "YourLedger-Stock-Topic" {
  name = "YourLedgerStockTopic"
}

resource "google_pubsub_topic" "YourLedger-Crypto-Topic" {
  name = "YourLedgerCryptoTopic"
}

resource "google_pubsub_subscription" "YourLedger-Subscription-Stock" {
  name  = "YourLedgerSubscriptionEquity"
  topic = google_pubsub_topic.YourLedger-Stock-Topic.name
  ack_deadline_seconds = 100
}

resource "google_pubsub_subscription" "YourLedger-Subscription-Crypto" {
  name  = "YourLedgerSubscriptionCrypto"
  topic = google_pubsub_topic.YourLedger-Crypto-Topic.name
  ack_deadline_seconds = 100
}

#Function
resource "google_cloudfunctions_function" "Equityfunction" {
  name        = "EquityUpdaterFunction"
  description = "This function is used to update the buy and sell orders of equity"
  runtime     = "dotnet3"
  project     = var.project-name
  available_memory_mb   = 256
  source_archive_bucket = google_storage_bucket.YourLedger-Function-bucket.name
  source_archive_object = google_storage_bucket_object.YourLedger-Function-Code.name
  entry_point           = "YourLedger.Functions.EquityUpdater"
  event_trigger          {
    event_type          = "google.pubsub.topic.publish"
    resource            = google_pubsub_topic.YourLedger-Stock-Topic.name
  }
}

resource "google_cloudfunctions_function" "Cryptofunction" {
  name        = "CryptoUpdaterFunction"
  description = "This function is used to update the buy and sell orders of crypto"
  runtime     = "dotnet3"
  project     = var.project-name
  available_memory_mb   = 256
  source_archive_bucket = google_storage_bucket.YourLedger-Function-bucket.name
  source_archive_object = google_storage_bucket_object.YourLedger-Function-Code.name
  entry_point           = "YourLedger.Functions.CryptoUpdater"
  event_trigger          {
    event_type          = "google.pubsub.topic.publish"
    resource            = google_pubsub_topic.YourLedger-Crypto-Topic.name
  }
}
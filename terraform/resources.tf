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
resource "google_pubsub_topic" "YourLedger-Topic" {
  name = "YourLedgerTopic"
}

resource "google_pubsub_subscription" "YourLedger-Subscription" {
  name  = "YourLedgerSubscription"
  topic = google_pubsub_topic.YourLedger-Topic.name
  ack_deadline_seconds = 100
}

#Function
resource "google_cloudfunctions_function" "function" {
  name        = "UpdaterFunction"
  description = "This function is used to update the buy and sell orders"
  runtime     = "dotnet3"
  project     = var.project-name
  available_memory_mb   = 256
  source_archive_bucket = google_storage_bucket.YourLedger-Function-bucket.name
  source_archive_object = google_storage_bucket_object.YourLedger-Function-Code.name
  entry_point           = "YourLedger.Functions.Updater"
  event_trigger          {
    event_type          = "google.pubsub.topic.publish"
    resource            = google_pubsub_topic.YourLedger-Topic.name
  }
}
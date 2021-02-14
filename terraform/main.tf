terraform {
  required_providers {
    google = {
      source = "hashicorp/google"
      version = "3.5.0"
    }
  }
}

provider "google" {

  credentials = file(var.gcp-creds)

  project = var.project-name
  region  = var.project-region
  zone    = var.project-zone
}
import random
from faker import Faker
import csv
from multiprocessing import Process
from enum import Enum

fake = Faker()

class Award(Enum):
    GOLD = 1
    PLATINUM = 2
    DIAMOND = 3

class Genre(Enum):
    HIPHOP = 1
    POP = 2
    ROCK = 3
    RandB = 4
    COUNTRY = 5
    JAZZ = 6
    BLUES = 7
    METAL = 8
    FOLK = 9
    ELECTRONIC = 10

MIN_YEAR = 1970
MAX_YEAR = 2023

ALBUMS_COUNT = 1_000_000
ARTISTS_COUNT = 1_000_000
RECORD_LABELS_COUNT = 1_000_000
CERTIFICATIONS_COUNT = 10_000_000

def create_albums_csv():
    print("Creating albums.csv...")
    with open("albums.csv", "w", newline="") as f:
        writer = csv.writer(f)

        for i in range(1, ALBUMS_COUNT+1):
            album_title = fake.sentence(nb_words=2, variable_nb_words=True)
            album_genre = random.choice(list(Genre)).name
            album_year = random.randint(MIN_YEAR, MAX_YEAR)
            album_price = random.randint(10, 40)
            album_number_of_tracks = random.randint(5, 20)
            album_artist_id = random.randint(1, ARTISTS_COUNT)
            album_record_label_id = random.randint(1, RECORD_LABELS_COUNT)

            writer.writerow([i, album_title, album_genre, album_year, album_price, album_number_of_tracks, album_artist_id, album_record_label_id])

    print("albums.csv created!")

def create_artists_csv():
    print("Creating artists.csv...")
    with open("artists.csv", "w", newline="") as f:
        writer = csv.writer(f)

        for i in range(1, ARTISTS_COUNT+1):
            artist_name = fake.name()
            artist_country = fake.country()
            artist_birth = fake.date_of_birth(minimum_age=18, maximum_age=100)
            artist_genre = random.choice(list(Genre)).name
            artist_recordlabel_id = random.randint(1, RECORD_LABELS_COUNT)

            writer.writerow([i, artist_name, artist_country, artist_birth, artist_genre, artist_recordlabel_id])
    
    print("artists.csv created!")

def create_record_labels_csv():
    print("Creating record_labels.csv...")
    with open("record_labels.csv", "w", newline="") as f:
        writer = csv.writer(f)

        for i in range(1, RECORD_LABELS_COUNT+1):
            record_label_name = fake.company()
            record_label_country = fake.country()
            record_label_date_of_foundation = fake.date_of_birth(minimum_age=18, maximum_age=40)
            record_label_ceo = fake.name()
            record_label_description = fake.paragraph(nb_sentences=3, variable_nb_sentences=True)
            record_label_number_of_artists = random.randint(1, 30)

            writer.writerow([i, record_label_name, record_label_country, record_label_date_of_foundation, record_label_ceo, record_label_number_of_artists,record_label_description])

    print("record_labels.csv created!")

def create_certifications_csv():
    print("Creating certifications.csv...")
    with open("certifications.csv", "w", newline="") as f:
        writer = csv.writer(f)

        for i in range(1, CERTIFICATIONS_COUNT+1):
            certification_award = random.choice(list(Award)).name
            certification_units_sold = random.randint(100_000, 1_000_000_000)
            certification_artist_id = random.randint(1, ARTISTS_COUNT)
            certification_record_label_id = random.randint(1, RECORD_LABELS_COUNT)

            writer.writerow([certification_record_label_id, certification_artist_id ,certification_award, certification_units_sold])

    print("certifications.csv created!")


if __name__ == "__main__":
    processes = []
    for func in [
        #create_albums_csv,
        #create_artists_csv,
        #create_record_labels_csv,
        create_certifications_csv
    ]:
        p = Process(target=func)
        processes.append(p)
        p.start()

    for p in processes:
        p.join()

    print("All done!")



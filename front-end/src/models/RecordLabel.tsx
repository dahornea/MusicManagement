import { Album } from "./Album";
import { Artist } from "./Artist";
import { Certification } from "./Certification";

export interface RecordLabel {
    RecordLabelId?: number;
    Name: string;
    Country: string;
    DateOfEstablishment: string;
    CEO: string;
    NumberOfArtists: number;
    Artists?: Artist[];
    Albums?: Album[];
    Certifications?: Certification[];
    [key: string]: any;
}  
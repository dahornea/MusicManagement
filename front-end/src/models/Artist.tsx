import { Album } from "./Album";
import { RecordLabel } from "./RecordLabel";
import {Certification} from "./Certification";

export interface Artist {
    ArtistId?: number;
    Name: string;
    Country: string;
    DateOfBirth: string;
    MainGenre: string;
    RecordLabelId?: number;
    RecordLabel?: RecordLabel;
    Albums?: Album[];
    Certifications?: Certification[];
    [key: string]: any;
}

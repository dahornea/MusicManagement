import {
    Button,
    Card,
    CardActions,
    CardContent,
    CircularProgress,
    Container,
    IconButton,
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
} from "@mui/material";

import { useEffect, useState } from "react";
import {Link, useNavigate, useParams} from "react-router-dom";
import {BACKEND_API_URL} from "../../constants";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import {Album} from "../../models/Album";
import axios, {AxiosError} from "axios";
import { Artist } from "../../models/Artist";
import { RecordLabel } from "../../models/RecordLabel";
import React from "react";

export const UpdateAlbum = () => {
    const [artist, setArtist] = useState<Artist[]>([]);
    const [recordLabel, setRecordLabel] = useState<RecordLabel[]>([]);
    const {albumId} = useParams<{albumId: string}>();
    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [album, setAlbum] = useState<Album>({
        Title: "",
        Genre: "",
        YearOfRelease: "",
        Price: 0,
        NumberOfTracks: 0,
        ArtistId: 1,
        RecordLabelId: 1,
    });

    useEffect(() => {
        const fetchAlbum = async () => {
            const response = await fetch(`${BACKEND_API_URL}/Album/${albumId}`);
            const album = await response.json();
            setAlbum({
                id: album.id,
                Title: album.title,
                Genre: album.genre,
                YearOfRelease: album.yearOfRelease,
                Price: album.price,
                NumberOfTracks: album.numberOfTracks,
                ArtistId: album.artistId,
                RecordLabelId: album.recordLabelId,
            });
            setLoading(false);
            }
            fetchAlbum();
        }, [albumId]);

    const handleUpdate = async(event: {preventDefault: () => void}) => {
        event.preventDefault();
        try{
            await axios
            .put(`${BACKEND_API_URL}/Album/${albumId}`, album)
            .then(() => {
                alert("Album updated successfully");
        })
        .catch((error: AxiosError) => {
            console.log(error.message);
            alert("Failed to update album");
        });
        navigate("/albums");
        } catch (error) {
            console.log(error);
            alert("Failed to update album");
        }
    };

    const handleCancel = (event: {preventDefault: () => void}) => {
        event.preventDefault();
        navigate("/albums");
    };

    return(
        <Container>
            <Card>
                <CardContent>
                    <IconButton component={Link} sx={{mr:3}} to="/albums">
                        <ArrowBackIcon />
                    </IconButton>
                    <form onSubmit={handleUpdate}>
                        <TextField
                           id="Title"
                            label="Title"
                            value={album.Title}
                            variant = "outlined"
                            fullWidth
                            sx = {{mb: 2}}
                            onChange={(event) =>
                                setAlbum({...album, Title: event.target.value})
                            }
                        />
                        <TextField
                            id="Genre"
                            label="Genre"
                            value={album.Genre}
                            variant = "outlined"
                            fullWidth
                            sx = {{mb: 2}}
                            onChange={(event) =>
                                setAlbum({...album, Genre: event.target.value})
                            }
                        />
                        <TextField
                            id="YearOfRelease"
                            label="YearOfRelease"
                            value={album.YearOfRelease}
                            variant = "outlined"
                            fullWidth
                            sx = {{mb: 2}}
                            onChange={(event) =>
                                setAlbum({...album, YearOfRelease: event.target.value})
                            }
                        />
                        <TextField
                            id="Price"
                            label="Price"
                            value={album.Price}
                            variant = "outlined"
                            fullWidth
                            sx = {{mb: 2}}
                            onChange={(event) =>
                                setAlbum({...album, Price: Number(event.target.value)})
                            }
                        />
                        <TextField
                            id="NumberOfTracks"
                            label="NumberOfTracks"
                            value={album.NumberOfTracks}
                            variant = "outlined"
                            fullWidth
                            sx = {{mb: 2}}
                            onChange={(event) =>
                                setAlbum({...album, NumberOfTracks: Number(event.target.value)})
                            }
                        />
                        <FormControl fullWidth>
                            <InputLabel id="ArtistLabel">Artist</InputLabel>
                            <Select
                                labelId="ArtistLabel"
                                id="ArtistId"
                                label="Artist"
                                value={album.ArtistId}
                                variant="outlined"
                                fullWidth
                                sx={{mb: 2}}
                                onChange={(event) =>
                                    setAlbum({...album, ArtistId: Number(event.target.value)})
                                }
                            >
                                {artist.map((artist) => (
                                    <MenuItem key={artist.id} value={artist.id}>
                                        {artist.name}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                        </form>
                        </CardContent>
                        <CardActions>
                            <CardActions sx={{justifyContent: "center"}}>
                                <Button type="submit" onClick={handleUpdate} variant="contained">Update</Button>
                                <Button onClick={handleCancel} variant="contained">Cancel</Button>
                            </CardActions>
                        </CardActions>                        
            </Card>
        </Container>
    );
};
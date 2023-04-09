export async function setImage(imageElementId, imageStream) {
    console.log("1");
    console.log(imageStream);
    
    const arrayBuffer = await imageStream.arrayBuffer();

    console.log("2");
    const blob = new Blob([arrayBuffer]);

    console.log("3");
    const url = URL.createObjectURL(blob);

    console.log("4");
    const image = document.getElementById(imageElementId);

    image.onload = () =>{
        URL.revokeObjectURL(url);
    }

    console.log("5");
    image.src = url;
}
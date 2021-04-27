import * as images from "../contains/images";

const initialState = {
    
};

export default (state = initialState, { type, payload }) => {
    switch (type) {
        case images.POST_IMAGES: {
            console.log(payload);
            return { ...state };

        }
        default:
            return state;
}
}
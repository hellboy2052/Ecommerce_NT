import React from "react";
export default function ProductList(props) {
  return (
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Name</th>
          <th scope="col">Price</th>
          <th scope="col">Image</th>
          <th scope="col">Option</th>
        </tr>
      </thead>
      <tbody>
        {props.list &&
          props.list.map((item) => {
            return (
              <tr>
                <th scope="row">{item.id}</th>
                <td>{item.name}</td>
                <td>{item.price}</td>
                <td><img style={{height:"50px"}} src={`https://localhost:44309${item.imageLocation[0]}`} alt="product image"/></td>
              </tr>
            );
          })}
      </tbody>
    </table>
  );
}

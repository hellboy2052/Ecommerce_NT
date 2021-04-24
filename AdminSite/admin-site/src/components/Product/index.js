import React from "react";
import { Table,Button } from 'reactstrap';
export default function ProductList(props) {
  return (
    <Table class="table">
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
                <td>
                <Button color="info">Update</Button>{" "}
                <Button color="danger" >
                  Delete
                </Button>
              </td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
}

import React, { Component } from "react";
import { constants } from "../../Constants";
import { MultiSelect } from "react-multi-select-component";
import { Button, Table, InputGroup, FormControl} from "react-bootstrap";

// Component for IngridientData page
export class IngridientsData extends Component {
	// Main constructor
	constructor(props) {
		super(props);

		// State initialization
		// Component state - properties of this component
		this.state = {
			// List of data to be displayed
			ingridientsData: [],
			categories: [],
			filterParameters: {
				ID: false,
				Name: false,
				Calories: false,
				Proteins: false,
				Carbohydrates: false,
				Fats: false
			},
			// Title of the modal window
			modalTitle: "",
			// Data of the selected item
			// This data will be displayed in the modal window
			itemID: 0,
			itemName: "",
			itemCalories: 0,
			itemProteins: 0,
			itemCarbohydrates: 0,
			itemFats: 0,
			itemImageName: "",
			itemCategories: [],
			// Options for multiselect
			selectOptions: [],
			// Data to display as multiselect selected
			categoryNames: [], 
			HTTPMethod: "",
			searchLine: ""
		};
	}

	// Called on Add Button click
	addClick() {
		// Clearing item data and saving current state
		this.setState({
			modalTitle: "Adding new Ingridient",
			itemID: 0,
			itemName: "",
			itemCalories: 0,
			itemProteins: 0,
			itemCarbohydrates: 0,
			itemFats: 0,
			itemCategories: [],
			itemImageName: "",
			selectOptions: this.getMultiSelectOptions(),
			categoryNames: [], 
			HTTPMethod: "POST"
		});
	}

	handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} 
		else {
			console.log(this.state.HTTPMethod);
			event.preventDefault();
			let ingridientCategories = [];

		for (let category of this.state.itemCategories) {
			ingridientCategories.push({IngridientId: this.state.itemID, CategoryId: category.value});
		}

		let method = this.state.HTTPMethod;

		fetch(constants.API_URL + "IngridientData", {
			method: method,
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				Id: this.state.itemID,
				Name: this.state.itemName,
				Calories: this.state.itemCalories,
				Proteins: this.state.itemProteins,
				Carbohydrates: this.state.itemCarbohydrates,
				Fats: this.state.itemFats,
				ImageName: this.state.itemImageName,
				ingridientCategory: ingridientCategories,
			})
		})
			.then(res => res.json())
			.then(
				result => {
					console.log(this.state.itemImageName,);
					this.refreshList();
				},
				error => {
					alert("Failed");
				}
			);
		}
	}

	// Save img in imgur and write img to object
	saveImg(ev) {
		const formdata = new FormData();
		formdata.append("image", ev.target.files[0]);
		console.log(ev.target.files[0]);
		fetch(constants.API_Imgur, {
			method: "POST",
			headers: {
				Authorization: "Client-ID " + constants.Client_ID,
				Accept: "application/json"
			},
			body: formdata
		})
			.then(res => res.json())
			.then(data => {
				console.log(data.data.link);
				this.setState({
					itemImageName: data.data.link,
				});
			});

			console.log(this.state.itemImageName);
	}

	editClick(row) {

		let rowCategories = this.state.ingridientsData.find(i => i.Id === row.Id)
			.IngridientCategory.map(i => i.Category);
		let temp = []

		for(let category of rowCategories) {
			temp.push({ label: category.Name, value: category.Id });
		}

		this.setState({
			modalTitle: "Editing Ingridient",
			itemID: row.Id,
			itemName: row.Name,
			itemCalories: row.Calories,
			itemProteins: row.Proteins,
			itemCarbohydrates: row.Carbohydrates,
			itemFats: row.Fats,
			itemCategories: temp,
			itemImageName: row.ImageName,
			selectOptions: this.getMultiSelectOptions(),
			categoryNames: rowCategories.map(i => i.Name),
			HTTPMethod: "PUT"
		});
	}

	// Called when delete button is clicked
	deleteClick(id) {
		// Confirmation pop-up
		if (window.confirm("Are you sure?")) {
			// Sending request to delete item from DB with current id
			fetch(constants.API_URL + "IngridientData/" + id, {
				method: "DELETE",
				headers: {
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						this.refreshList();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// HTTP GET request, retrieves data from server
	// and saves it to the component state
	refreshList() {
		fetch(constants.API_URL + "IngridientData")
			.then(response => response.json())
			.then(data => {
				this.setState({ ingridientsData: data });
			});

		fetch(constants.API_URL + "Category")
			.then(response => response.json())
			.then(data => {
				this.setState({ categories: data });
			});
	}

	// Don't exactly know what is it :)
	// Maybe it is called when component is rendered ¯\_(ツ)_/¯
	componentDidMount() {
		this.refreshList();
	}

	changeCategory = e => {
		this.setState({ 
			itemCategories: e,
			categoryNames: e.map(e => e.label),
		});

	};

	getMultiSelectOptions() {
		let res = [];

		for (let category of this.state.categories) {
			res.push({label: category.Name, value: category.Id });
		}

		return res;
	}

	// Sort functions
	sortById() {
		let filterParameters = this.state.filterParameters;
		if (filterParameters.Id) {
			this.state.ingridientsData.sort((a, b) => (a.Id > b.Id ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: false,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		} else {
			this.state.ingridientsData.sort((a, b) => (a.Id < b.Id ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: true,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		}
	}
	sortByName() {
		let filterParameters = this.state.filterParameters;
		if (filterParameters.Name) {
			this.state.ingridientsData.sort((a, b) => a.Name.localeCompare(b.Name));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: false,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		} else {
			this.state.ingridientsData.sort((b, a) => a.Name.localeCompare(b.Name));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: true,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		}
	}
	sortByCalories() {
		let filterParameters = this.state.filterParameters;
		if (filterParameters.Calories) {
			this.state.ingridientsData.sort((a, b) => (a.Calories > b.Calories ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: false,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		} else {
			this.state.ingridientsData.sort((a, b) => (a.Calories < b.Calories ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: true,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		}
	}
	sortByFats() {
		let filterParameters = this.state.filterParameters;
		if (filterParameters.Fats) {
			this.state.ingridientsData.sort((a, b) => (a.Fats > b.Fats ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: false,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		} else {
			this.state.ingridientsData.sort((a, b) => (a.Fats < b.Fats ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: true,
					Proteins: filterParameters.Proteins,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		}
	}
	sortByProteins() {
		let filterParameters = this.state.filterParameters;
		if (filterParameters.Proteins) {
			this.state.ingridientsData.sort((a, b) => (a.Proteins > b.Proteins ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: false,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		} else {
			this.state.ingridientsData.sort((a, b) => (a.Proteins < b.Proteins ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: true,
					Carbohydrates: filterParameters.Carbohydrates
				}
			});
		}
	}
	sortByCarbohydrates() {
		let filterParameters = this.state.filterParameters;
		if (filterParameters.Carbohydrates) {
			this.state.ingridientsData.sort((a, b) => (a.Carbohydrates > b.Carbohydrates ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: false
				}
			});
		} else {
			this.state.ingridientsData.sort((a, b) => (a.Carbohydrates < b.Carbohydrates ? 1 : -1));
			this.setState({
				filterParameters: {
					Id: filterParameters.Id,
					Name: filterParameters.Name,
					Calories: filterParameters.Calories,
					Fats: filterParameters.Fats,
					Proteins: filterParameters.Proteins,
					Carbohydrates: true
				}
			});
		}
	}

	// Possibly main function of the component
	// Return statement of this function describes what
	// will be displayed in place where this component is used
	render() {
		// This part describes what will be displayed
		return (
			<div className="container">
				<div style={{ width: 80 + "vw" }}>
					<h3 className="m-5">This is Categories page</h3>

					{/* Main table */}
					<div className="container mt-5">
						<InputGroup className="mb-3">
							<FormControl
								aria-label="Default"
								placeholder="Search"
								value={this.state.searchLine}
								onChange={e => this.setState( { searchLine: e.target.value }) }
								aria-describedby="inputGroup-sizing-default"
							/>
							<Button className="btn-dark" onClick={ () =>  { 
								this.setState({
									ingridientsData: this.state.ingridientsData
										.filter(i => i.Name.includes(this.state.searchLine))
								});
							}}>Search</Button>
							<Button className="btn-dark" onClick={() => this.refreshList()}>Cancel</Button>
						</InputGroup>
						<Table 
							className="table table-striped table-bordered table-sm text-center" 
							striped bordered hover size="lg"
							id="dtBasicExample">
							<thead>
								<tr>
									<th class="th-sm">
										ID
										<Button className="btn-light" onClick={() => this.sortById()}>
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th class="th-sm">
										Name
										<Button className="btn-light" onClick={() => this.sortByName()}>
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th>
										Image
									</th>
									<th class="th-sm">
										Calories
										<Button className="btn-light" onClick={() => this.sortByCalories()}>
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th class="th-sm">
										Proteins
										<Button className="btn-light" onClick={() => this.sortByProteins()}>
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th class="th-sm">
										Fats
										<Button className="btn-light" onClick={() => this.sortByFats()}>
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th class="th-sm">
										Carbohydrates
										<Button className="btn-light" onClick={() => this.sortByCarbohydrates()}>
											<i className="fa-solid fa-arrows-up-down"></i>
										</Button>
									</th>
									<th class="th-sm">Options</th>
								</tr>
							</thead>
							<tbody>
								{this.state.ingridientsData.map(e => (
									<tr key={e.Id}>
										<td>{e.Id}</td>
										<td>{e.Name}</td>
										<td><img src={e.ImageName} alt="img" width="100px" /></td>
										<td>{e.Calories}</td>
										<td>{e.Proteins}</td>
										<td>{e.Fats}</td>
										<td>{e.Carbohydrates}</td>
										<td>
											<Button 
												className="m-2" 
												variant="outline-dark"
												onClick={() => this.editClick(e)}
												data-bs-toggle="modal"
												data-bs-target="#exampleModal">
												Edit
											</Button>
											<Button className="m-2" onClick={() => this.deleteClick(e.Id)} variant="outline-danger">
												Delete
											</Button>
										</td>
									</tr>
								))}
							</tbody>
						</Table>
					</div>

					{/* Three buttons to perform add operation */}
					<button
						type="button"
						className="btn btn-dark m-2 float-end"
						// Click will trigger modal
						data-bs-toggle="modal"
						// Id of modal to be triggered
						data-bs-target="#exampleModal"
						onClick={() => this.addClick()}>
						Add ingridient data
					</button>

					{/* Modal window component */}
					<div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
						<div className="modal-dialog modal-lg modal-dialog-centered">
							<div className="modal-content">
								<div className="modal-header">
									<h5 className="modal-title">{this.state.modalTitle}</h5>
									<button
										type="button"
										className="btn-close"
										data-bs-dismiss="modal"
										aria-label="Close"></button>
								</div>
								<div className="modal-body">
									<form class="needs-validation" id="modalForm" noValidate onSubmit={this.handleSubmit}>
									{/* Inputs for item properties
                                    value: assigns data from component state
                                    onChange: calls specified function to update state */}
										<div className="form-group mb-3">
											<span className="form-label">Ingridient Name</span>
											<input
												required
												type="text"
												placeholder="Name"
												pattern="^([A-ZА-ЯЁ]||[a-zа-яё])*$"
												className="form-control"
												value={this.state.itemName}
												onChange={e => this.setState({ itemName: e.target.value }) }
											/>
										</div>
										<div className="form-group mb-3">
											<span className="form-label">Ingridient Calories</span>
											<input
												required
												type="text"
												placeholder="Calories"
												pattern="^[1-9][0-9]*$"
												className="form-control"
												value={this.state.itemCalories}
												onChange={e => this.setState({ itemCalories: e.target.value }) }
											/>
										</div>
										<div className="form mb-3">
											<span className="form-label">Ingridient Carbohydrates</span>
											<input
												required
												type="text"
												placeholder="Carbohydrates"
												pattern="^[1-9][0-9]*$"
												className="form-control"
												value={this.state.itemCarbohydrates}
												onChange={e => this.setState({ itemCarbohydrates: e.target.value }) }
											/>
										</div>
										<div className="form mb-3">
											<span className="form-label">Ingridient Fats</span>
											<input
												required
												type="text"
												placeholder="Fats"
												pattern="^[1-9][0-9]*$"
												className="form-control"
												value={this.state.itemFats}
												onChange={e => this.setState({ itemFats: e.target.value }) }
											/>
										</div>
										<div className="form mb-3">
											<span className="form-label">Ingridient Proteins</span>
											<input
												required
												type="text"
												placeholder="Proteins"
												pattern="^[1-9][0-9]*$"
												className="form-control"
												value={this.state.itemProteins}
												onChange={e => this.setState({ itemProteins: e.target.value }) }
											/>
										</div>
										<div className="form mb-3">
											{/* <span className="form-text">Ingridient Category</span> */}
											
											<MultiSelect
												className="form-label"
												options={this.state.selectOptions}
												value={this.state.itemCategories}
												onChange={this.changeCategory}
												labelledBy="Ingridient Category"
											/>
											<input
												type="text"
												className="form-control"
												value={JSON.stringify(this.state.categoryNames)}
												onChange={this.changeProteins}
												disabled={true}
											/>
										</div>
										{/* if the object is being edited - display an image */}
										{this.state.HTTPMethod === "PUT" && (
											<div className="container border mb-3">
												<p>Image now</p>
												<img src={this.state.itemImageName} alt="unloaded img" width="150px" />
											</div>
										)}
										<div className="form mb-3">
											<span className="form-label">Image</span>
											<input
												className="form-control"
												type="file"
												placeholder="ImageName"
												//value={this.state.itemImageName}
												onChange={e => this.saveImg(e)}
											/>
										</div>

										{/* If selected item id == 0 Than we need to add new item */}
										{this.state.itemID === 0 ? (
											<button
												type="submit"
												className="btn btn-dark float-end"
												>
												Create
											</button>
										) : null}

										{/* If selected item id !== 0 Than we need to updating existing item */}
										{this.state.itemID !== 0 ? (
											<button
												type="submit"
												className="btn btn-dark float-end"
												>
												Update
											</button>
										) : null}
									</form>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		);
	}
}

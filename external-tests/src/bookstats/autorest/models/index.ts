/*
 * Code generated by Microsoft (R) AutoRest Code Generator.
 * Changes may cause incorrect behavior and will be lost if the code is regenerated.
 */

import { ServiceClientOptions } from "@azure/ms-rest-js";
import * as msRest from "@azure/ms-rest-js";

/**
 * An interface representing Stats.
 */
export interface Stats {
  bookId?: number;
  copiesSold?: number;
}

/**
 * An interface representing BookStatsApiOptions.
 */
export interface BookStatsApiOptions extends ServiceClientOptions {
  baseUri?: string;
}

/**
 * Contains response data for the get operation.
 */
export type StatsGetResponse = Stats & {
  /**
   * The underlying HTTP response.
   */
  _response: msRest.HttpResponse & {
      /**
       * The response body as text (string format)
       */
      bodyAsText: string;

      /**
       * The response body as parsed JSON or XML
       */
      parsedBody: Stats;
    };
};
